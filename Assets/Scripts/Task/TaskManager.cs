using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务管理器 创建新任务 当等级变化时更新主线任务
/// 主线任务
/// 支线任务
/// </summary>
public class TaskManager : UnitySingleTon<TaskManager>
{
    public Dictionary<string,taskItem> mainTaskItemDict = new Dictionary<string,taskItem>();//主线任务字典
    public Dictionary<string,taskItem> branchTaskItemDict = new Dictionary<string,taskItem>();//支线任务字典

    public override void Awake()
    {
        base.Awake();

        // 延迟一帧初始化，确保所有单例已就绪
        StartCoroutine(DelayedPanelInit());
    }


    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.玩家等级发生变化,UpdateMainTask);
        EventCenter.Instance.RemoveEventListener<taskItem>(GameEvent.任务结束事件,JiangLi);
        EventCenter.Instance.RemoveEventListener(GameEvent.任务完成事件,CheckUpdateGrade);
    }

    /// <summary>
    /// 初始化任务数据
    /// </summary>
    public void Init()
    {
        EventCenter.Instance.AddEventListener(GameEvent.玩家等级发生变化,UpdateMainTask);
        EventCenter.Instance.AddEventListener<taskItem>(GameEvent.任务结束事件,JiangLi);
        EventCenter.Instance.AddEventListener(GameEvent.任务完成事件,CheckUpdateGrade);
        UpdateTask();
    }

    /// <summary>
    /// 更新任务列表
    /// </summary>
    public void UpdateTask()
    {
        UpdateMainTask();
        UpdateBranchTask();
    }

    private IEnumerator DelayedPanelInit()
    {
        yield return null; // 等待一帧
        UIManager.Instance.openPanel<TaskPanel>();
        // 等待面板完全初始化
        yield return new WaitForEndOfFrame();
    
        Init(); // 在面板初始化后初始化任务
    }

    /// <summary>
    /// 等级发生变化后更新主线任务信息 初始时更新一次
    /// </summary>
    public void UpdateMainTask()
    {
        //拿到下一个等级编号
        int level = GameManager.instance.CurrPlayerData.GameLevel + 1;
        //显示等级图标 MARKER
        
        //遍历主线任务字典，删除之前的任务信息
        foreach (var taskItem in mainTaskItemDict.Values)
        {
            Destroy(taskItem.gameObject);
        }
        //清空主线任务字典
        mainTaskItemDict.Clear();
        //添加新等级的任务到主线任务字典中
        foreach (var taskItem in GameManager.instance.taskItemDict.Values)
        {
            if (taskItem.level == level && taskItem.type == "主线任务")
            {
                CreateNewTask(taskItem.id);
            }
        }

    }
    
    /// <summary>
    /// 更新支线任务信内容
    /// </summary>
    public void UpdateBranchTask()
    {
        foreach (var taskItem in branchTaskItemDict.Values)
        {
            Destroy(taskItem.gameObject);
        }
        branchTaskItemDict.Clear();
        foreach (var taskItem in GameManager.instance.taskItemDict.Values)
        {
            if (taskItem.level<=GameManager.instance.CurrPlayerData.GameLevel && taskItem.type == "支线任务")
            {
                CreateNewTask(taskItem.id);
            }
        }
    }
    
    /// <summary>
    /// 创建新任务 将任务项添加到对应的任务列表中 比如主线任务和支线任务面板
    /// </summary>
    public void CreateNewTask(string id)
    {
        //更改任务信息的开始状态
        GameManager.instance.taskItemDict[id].isStarted = true;
        //触发任务开始事件 主线任务和支线任务面板执行相应的更新方法
        EventCenter.Instance.EventTrigger<string>(GameEvent.任务开始事件,id);
    }

    /// <summary>
    /// 触发完成任务后的奖励
    /// </summary>
    public void JiangLi(taskItem taskItem)
    {
        if (taskItem == null)
        {
            return;
        }
        
        taskItem.TaskOver();//任务结束
        //领取奖励
        UIManager.Instance.openPanel<TipPanel>().UpdateTipText("完成任务获得任务奖励");
        //任务完成
        taskItem.TaskFinish();
        
    }

    /// <summary>
    /// 核对更新等级的方法 当任务事件触发的时候会调用 判断档期啊所有任务是否都完成了
    /// </summary>
    public void CheckUpdateGrade()
    {
        foreach (var taskItem in mainTaskItemDict.Values)
        {
            if (taskItem.taskItemData.isFinished == false)
            {
                return;
            }
        }

        GameManager.instance.CurrPlayerData.GameLevel++;
    }
    
    /// <summary>
    /// 清空所有任务 
    /// </summary>
    public void clearAllTask()
    {
        ClearMainTask();
        ClearBranchTask();  
    }
    
    /// <summary>
    /// 清空主线任务   
    /// </summary>
    public void ClearMainTask()
    {
        foreach (taskItem taskItem in mainTaskItemDict.Values)
        {
            Destroy(taskItem.gameObject);
        }
        mainTaskItemDict.Clear();   
    }
    
    /// <summary>
    /// 清空支线任务 
    /// </summary>
    public void ClearBranchTask()
    {
        foreach (taskItem taskItem in branchTaskItemDict.Values)
        {
            Destroy(taskItem.gameObject);
        }        
        branchTaskItemDict.Clear(); 
    }
}
