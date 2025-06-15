using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TaskPanel : BasePanel
{
    [Header("主线任务面板")]
    public GameObject MainTaskPanel;
    [Header("支线任务面板")]
    public GameObject BranchTaskPanel;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(GameEvent.任务UI切换,DelayUI);
    }

    private void DelayUI()
    {
        StartCoroutine(DelayedLayoutUpdate());
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.任务UI切换,DelayUI);
    }
}
