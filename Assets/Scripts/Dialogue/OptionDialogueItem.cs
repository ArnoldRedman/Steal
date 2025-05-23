using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对话选项
/// </summary>
public class OptionDialogueItem : DialogueItem
{
    [Header("任务按钮")]
    public Button checkBtn;
    [Header("任务文本")]
    public Text text;

    void Start()
    {
        checkBtn.onClick.AddListener(UpdateNext);
    }

    private void UpdateNext()
    {
        EventCenter.Instance.EventTrigger<string,string>(GameEvent.切换到下一条对话语句,dialogueItemData.nextId,dialogueItemData.taskId);
    }

    /// <summary>
    /// 选项内容更新 动态生成Option时调用
    /// </summary>
    /// <param name="dialogueId"></param>
    public void UpdateOptionData(string dialogueId)
    {
        id = dialogueId;
        dialogueItemData = GameManager.instance.dialogueItemDict[id];
        text.text = dialogueItemData.dialogueContent;
    }
}
