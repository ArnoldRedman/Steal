using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主线任务面板
/// </summary>
public class MainTask : Task
{
    [Header("要达到的等级的图标")] public Image levelIconl;

    private void OnEnable()
    {
        taskType = "主线任务";
        UpdateGradeData();
        EventCenter.Instance.AddEventListener<string>(GameEvent.任务开始事件,UpdateTaskItem);
        EventCenter.Instance.AddEventListener(GameEvent.玩家等级发生变化,UpdateGradeData);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<string>(GameEvent.任务开始事件,UpdateTaskItem);
        EventCenter.Instance.RemoveEventListener(GameEvent.玩家等级发生变化,UpdateGradeData);
    }

    /// <summary>
    /// 更新等级信息
    /// </summary>
    private void UpdateGradeData()
    {
        int level = GameManager.instance.CurrPlayerData.GameLevel + 1;
        switch (level)
        {
            case 1:
                
                levelIconl.sprite = Resources.Load<Sprite>("Sprite/等级一");
                taskBtnText.text = "达到等级一目标";
                
                break;
            
            case 2:
                
                levelIconl.sprite = Resources.Load<Sprite>("Sprite/等级二");
                taskBtnText.text = "达到等级二目标";
                
                break;
            
            case 3:
                
                levelIconl.sprite = Resources.Load<Sprite>("Sprite/等级二");
                taskBtnText.text = "达到等级二目标";
                
                break;
            
            default:
                taskBtnText.text = "当前无任务";
                break;
        }
    }
}
