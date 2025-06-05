using System;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    [Header("加载存档")]
    public Button loadFileBtn;
    [Header("新开游戏按钮")]
    public Button newGameBtn;
    [Header("设置按钮")]
    public Button settingBtn;
    [Header("退出按钮")]
    public Button exitBtn;

    private void Start()
    {
        newGameBtn.onClick.AddListener(NewGame);
        loadFileBtn.onClick.AddListener(LoadGame);
        exitBtn.onClick.AddListener(ExitGame);
    }
    
    /// <summary>
    /// 新开存档
    /// </summary>
    private void NewGame()
    {
        GameManager.instance.mainSceneData = null;//新开游戏 主场景的数据是空的
        //初始化主场景数据
        GameManager.instance.MainSceneInit();
        //关闭开始面板
        UIManager.Instance.closePanel<StartPanel>();
        //跳转到游戏场景
        SceneMgr.Instance.LoadSceneAsync("MainScene",(() =>
        {
            UIManager.Instance.closePanel<LoadPanel>();
            //打开玩家数据面板
            UIManager.Instance.openPanel<PlayerPropPanel>();
            //打开任务数据面板
            UIManager.Instance.openPanel<TaskPanel>();
            //初始化任务面板数据 
            TaskManager.Instance.UpdateTask();
        }));
    }
    
    /// <summary>
    /// 加载存档
    /// </summary>
    private void LoadGame()
    {
        
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
