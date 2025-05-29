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
        EventCenter.Instance.AddEventListener<string>(GameEvent.任务开始事件,UpdateTaskItem);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<string>(GameEvent.任务开始事件,UpdateTaskItem);
    }
}
