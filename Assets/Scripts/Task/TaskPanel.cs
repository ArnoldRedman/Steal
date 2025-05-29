using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务面板
/// </summary>
public class TaskPanel : BasePanel
{
    [Header("主线任务面板")] 
    public GameObject mainTaskPanel;
    [Header("支线任务面板")] 
    public GameObject branchTaskPanel;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(GameEvent.任务UI切换,DelayUI);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.任务UI切换,DelayUI);
    }

    public void DelayUI()
    {
        StartCoroutine(DelayedLayoutUpdate());
    }
}
