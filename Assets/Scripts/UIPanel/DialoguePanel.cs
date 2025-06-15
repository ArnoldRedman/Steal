using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DialoguePanel : BasePanel
{
    [Header("当前对话对象名字")] public Text nameText;
    [Header("对话对象图标")] public Image icon;
    [Header("对话内容")] public Text dialogueText;
    [Header("下一句话的按钮")] public Button nextBtn;
    [Header("选项的容器")] public GameObject options;
    [Header("对话的容器")] public GameObject dialogueContent;
    public DialogueItemData currentDialogueData;

    private void Start()
    {
     
    }
    private void OnEnable()
    {
        StartCoroutine(DelayedLayoutUpdate());
        nextBtn.onClick.AddListener(() =>
        {
            UpdateNextDialogue(currentDialogueData.nextId,currentDialogueData.taskId);
        });
        EventCenter.Instance.AddEventListener<string,string>(GameEvent.切换下一条对话语句,UpdateNextDialogue);
    }

    private void OnDisable()
    {
        StopCoroutine(DelayedLayoutUpdate());
        nextBtn.onClick.RemoveAllListeners();
        EventCenter.Instance.RemoveEventListener<string,string>(GameEvent.切换下一条对话语句,UpdateNextDialogue);
    }

    private void Init()
    {
        dialogueContent.gameObject.SetActive(false);
        options.SetActive(false);
        nextBtn.gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新下一个对话内容的方法  
    /// </summary>
    /// <param name="id">下一条语句的id</param>
    /// <param name="taskId">当前点到的语句的任务id</param>
    public void UpdateNextDialogue(string id,string taskId="0")
    {
        //判断任务id 领取任务  
        if(taskId!="0")TaskManager.Instance.creatNewBranchTask(taskId);
        //判断是否有下一条语句 没有就关闭对话框
        if (id == "0")
        {
            DialogueManager.Instance.currentNPC = null;
            UIManager.Instance.closePanel<DialoguePanel>();
            return;
        }

        Init();
        //显示对话内容
        dialogueContent.SetActive(true);
        currentDialogueData = GameManager.Instance.dialogueItemDict[id];
        //更新对话内容  
        nameText.text = currentDialogueData.targetName;
        icon.sprite = ResMgr.Instance.load<Sprite>("Icon/StaffIcon/" + currentDialogueData.targetIcon);
        //更新选项的内容  更新选项内容是在对话内容显示结束之后才会显示选项内容 
        dialogueText.text = "";
        dialogueText.DOText(currentDialogueData.dialogueContent, currentDialogueData.dialogueContent.Length * 0.23f)
            .SetEase(Ease.Linear)
            .OnComplete(FinishText);
        
        
    }
/// <summary>
/// 播放文字结束后执行的方法
/// </summary>
    private void FinishText()
    {
        UpdateOptions();
    }

    /// <summary>
/// 更新选项的内容
/// </summary>
    private void UpdateOptions()
    {
        
        for (int i = 0; i < options.transform.childCount; i++)
        {
            Destroy(options.transform.GetChild(i).gameObject);
        }

        if (currentDialogueData.optionList.Count > 0)
        {
            nextBtn.gameObject.SetActive(false);
            dialogueContent.SetActive(false);
            options.SetActive(true);
            //初始化一下options  
            foreach (var id in currentDialogueData.optionList)
            {
                GameObject option = ResMgr.Instance.load<GameObject>("UI/OptionItemBtn", options.transform);
                option.GetComponent<OptionDialogueItem>().UpdateOptionData(id);
            }
        }
        else
        {
            nextBtn.gameObject.SetActive(true);
        }
    }
}