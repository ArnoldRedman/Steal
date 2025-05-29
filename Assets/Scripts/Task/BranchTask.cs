using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 支线任务面板
/// </summary>
public class BranchTask : Task
{
    private void OnEnable()
    {
        taskType = "支线任务";
        EventCenter.Instance.AddEventListener<string>(GameEvent.任务开始事件,UpdateTaskItem);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<string>(GameEvent.任务开始事件,UpdateTaskItem);
    }
}
