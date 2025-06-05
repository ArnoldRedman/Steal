using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏暂停的面板
/// </summary>
public class GamePausePanel : BasePanel
{
    [Header("打开存档界面按钮")]
    public Button saveBtn;
    [Header("取消存档按钮")]
    public Button cancelBtn;

    private void Start()
    {
        saveBtn.onClick.AddListener(NewFile);
        cancelBtn.onClick.AddListener(BackToStartScene);
    }

    /// <summary>
    /// 跳转到开始场景
    /// </summary>
    private void BackToStartScene()
    {
        //保存存档信息
        
        //清空所有面板
        UIManager.Instance.ClearAllPanel();
        //清空所有任务 这里清空面板是将面板取消激活，面板中的任务项都是还在的 所以需要在这里移除任务信息
        TaskManager.Instance.clearAllTask();
        SceneMgr.Instance.LoadSceneAsync("StartScene", () =>
        {
            UIManager.Instance.closePanel<LoadPanel>();
            UIManager.Instance.openPanel<StartPanel>();
        });
    }

    private void NewFile()
    {
        
    }
}
