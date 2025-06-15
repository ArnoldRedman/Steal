using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePausePanel : BasePanel
{
    [Header("返回主界面按钮")]
    public Button backBtn;
    [Header("新开存档按钮")]
    public Button newFileBtn;
    private void Start()
    {
        backBtn.onClick.AddListener(backToStartScene);
        newFileBtn.onClick.AddListener(newFile);
    }
/// <summary>
/// 新开存档 
/// </summary>
    private void newFile()
    {
        UIManager.Instance.closePanel<GamePausePanel>();
        UIManager.Instance.openPanel<SaveFilePanel>();
    }

    /// <summary>
/// 返回主界面场景
/// </summary>
    private void backToStartScene()
    {
        //保存存档信息  
        GameManager.Instance.SaveArchive();
        //清空所有面板
        UIManager.Instance.clearAllPanel();
        //清空所有任务 
        TaskManager.Instance.clearAllTask();        
        SceneMgr.Instance.LoadSceneAsync("StartScene", () =>
        {
            UIManager.Instance.closePanel<LoadPanel>();
            UIManager.Instance.openPanel<StartPanel>();
        });
    }

    public override void OnDestroy()
    {
        base.OnDestroy();   
        backBtn.onClick.RemoveAllListeners();
    }
    
}
