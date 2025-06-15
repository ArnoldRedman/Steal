using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// 游戏初始面板
/// </summary>
public class StartPanel : BasePanel
{
    [Header("加载存档按钮")] public Button loadBtn;
    [Header("新开游戏按钮")] public Button newGameBtn;
    [Header("设置按钮")] public Button settingsBtn;
    [Header("退出按钮")] public Button exitBtn;

    void Start()
    {
        exitBtn.onClick.AddListener(exitGame);//退出游戏按钮
        newGameBtn.onClick.AddListener(newGame);//新开游戏
        loadBtn.onClick.AddListener(loadGame);
    }


    public override void OnDestroy()
    {
        exitBtn.onClick.RemoveAllListeners();
        newGameBtn.onClick.RemoveAllListeners();
        loadBtn.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// 加载存档
    /// </summary>
    private void loadGame()
    {
        UIManager.Instance.openPanel<LoadFilePanel>();
    }

    /// <summary>
/// 新开游戏 跳转场景到主场景 
/// </summary>
    private void newGame()
    {
        GameManager.Instance.mainSceneData = null;
        //初始化主场景游戏数据  
        GameManager.Instance.MainSceneInit();
        //关闭开始面板 
        UIManager.Instance.closePanel<StartPanel>();
        //跳转到mainscene场景
        SceneMgr.Instance.LoadSceneAsync("MainScene", () =>
        {
            UIManager.Instance.closePanel<LoadPanel>();
            //玩家数据面板
            UIManager.Instance.openPanel<PlayerPropPanel>();
            //任务数据面板
            UIManager.Instance.openPanel<TaskPanel>();
            //初始化任务面板数据
            TaskManager.Instance.Init();    
            //初始化主场景中的游戏对象 
            
        });
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    private void exitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;    
#else
        Application.Quit();
#endif
    }


}