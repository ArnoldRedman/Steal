using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialoguePanel : BasePanel
{
    [Header("当前对话对象名字")] public Text nameText;
    [Header("对话对象图标")] public Image icon;
    [Header("对话内容")] public Text dialogueText;
    [Header("下一句话的按钮")] public Button nextBtn;
    [Header("选项的容器")] public GameObject options;
    [Header("对话的容器")] public GameObject dialogueContent;
    public DialogueItemData currentDialogueData;

    private void OnEnable()
    {
        StartCoroutine(DelayedLayoutUpdate());
        nextBtn.onClick.AddListener(() =>
        {
            UpdateNextDialogue(currentDialogueData.nextId,currentDialogueData.taskId);
        });
        
        EventCenter.Instance.AddEventListener<string,string>(GameEvent.切换到下一条对话语句,UpdateNextDialogue);
    }

    private void OnDisable()
    {
        StopCoroutine(DelayedLayoutUpdate());
        EventCenter.Instance.RemoveEventListener<string,string>(GameEvent.切换到下一条对话语句,UpdateNextDialogue);   
    }

    /// <summary>
    /// 切换到下一条语句的方法
    /// </summary>
    /// <param name="id">下一条语句的id</param>
    /// <param name="taskId">当前语句发布的任务id</param>
    public void UpdateNextDialogue(string id,string taskId = "0")
    {
        //判断任务id 如果有任务就要去接收任务了
        //任务的触发条件有可能是剧情对话结束后发布的任务
        //也有可能是玩家在选项中自己选定的任务

        //当前就是最后一句话 没有下一条语句了
        if (id == "0")
        {
            DialogueManager.Instance.currNPC = null;//对话结束了 对话对象重置
            UIManager.Instance.closePanel<DialoguePanel>();
            return;
        }

        Init();
        //显示对话内容
        dialogueContent.SetActive(true);
        currentDialogueData = GameManager.instance.dialogueItemDict[id];
        //更新对话内容
        nameText.text = currentDialogueData.targetName;
        icon.sprite = ResMgr.Instance.load<Sprite>("Icon/StaffIcon/" + currentDialogueData.targetIcon);
        //更新完对话内容之后 显示选项
        dialogueText.text = "";
        //DOTween的DoText可以让字符显示出来具有打字机的效果
        dialogueText.DOText(currentDialogueData.dialogueContent, //需要显示的字符串
            currentDialogueData.dialogueContent.Length * 0.17f //持续事件，长度*0.3f,相当于每个字是0.3s显示完
        ).SetEase(Ease.Linear).OnComplete(FinishText);//效果结束之后的回调函数
    }

    /// <summary>
    /// 文字打印结束之后执行的方法
    /// </summary>
    private void FinishText()
    {
        //更新选项选项
        UpdateOptions();
    }

    /// <summary>
    /// 更新选项信息
    /// </summary>
    private void UpdateOptions()
    {
        //添加新的选项
        //先判断有没有选项
        if (currentDialogueData.optionList.Count > 0)
        {
            nextBtn.gameObject.SetActive(false);
            dialogueContent.gameObject.SetActive(false);
            options.gameObject.SetActive(true);
            //删除之前的选项
            for (int i = 0; i < options.transform.childCount; i++)
            {
                Destroy(options.transform.GetChild(i).gameObject);
            }
            //初始化option的内容
            //选项里面放的是对话id
            foreach (var id in currentDialogueData.optionList)
            {
                GameObject option = ResMgr.Instance.load<GameObject>("UI/OptionItemBtn",options.transform);
                option.GetComponent<OptionDialogueItem>().UpdateOptionData(id);
            }
        }
        else//没有选项就显示下一句的按钮
        {
            nextBtn.gameObject.SetActive(true);
        }
    }

    private void Init()
    {
        dialogueContent.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
    }
}
