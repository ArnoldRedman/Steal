using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务项 任务的信息 当创建一个新的任务的时候 初始化任务的信息
/// </summary>
public class taskItem : MonoBehaviour
{
    [Header("任务需求描述")] 
    public Text taskDes;
    [Header("需要完成任务的图标")] 
    public Image ItemImg;
    [Header("当前完成的数量")] 
    public Text currentCountText;
    [Header("需要完成的总数量")]
    public Text countText;
    [Header("打勾图标")]
    public Image isFinishIcon;

    public TaskItemData taskItemData;//任务的数据
    public NumDemandData currentDemandData;//需求数据

    private void Start()
    {
        EventCenter.Instance.AddEventListener(GameEvent.建造物数量变化,UpdateNumData);
        EventCenter.Instance.AddEventListener(GameEvent.金币发生改变,UpdateNumData);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.建造物数量变化,UpdateNumData);
        EventCenter.Instance.RemoveEventListener(GameEvent.金币发生改变,UpdateNumData);
    }

    /// <summary>
    /// 任务结束的时候执行的方法
    /// </summary>
    public void TaskOver()
    {
        taskItemData.isEnd = true;
        GameManager.instance.taskItemDict[taskItemData.id].isEnd = true;
    }

    /// <summary>
    /// 任务完成执行的方法
    /// </summary>
    public void TaskFinish()
    {
        taskItemData.isFinished = true;
        GameManager.instance.taskItemDict[taskItemData.id].isFinished = true;
        //更新图标
        UpdateCheckIcon();
        //执行任务完成的事件
        EventCenter.Instance.EventTrigger(GameEvent.任务完成事件);//有可能做完这个任务所有任务都完成了 触发等级切换事件
    }

    /// <summary>
    /// 更新任务数据的方法 在创建任务的时候调用
    /// </summary>
    /// <param name="id"></param>
    public void UpdateTaskItem(string id)
    {
        //拿到任务信息
        taskItemData = GameManager.instance.taskItemDict[id];
        //拿到需求信息
        currentDemandData = GameManager.instance.GetNumDemandData(taskItemData.demandDict["numdemand"]);
        //更新需求描述
        taskDes.text = currentDemandData.description;
        //根据物品类型来显示不同的对应图标
        switch (currentDemandData.itemType)
        {
            case "building":
                
                BuildItemData build = GameManager.instance.buildItemDict[currentDemandData.itemId];
                ItemImg.sprite = ResMgr.Instance.load<Sprite>("Sprite/" + build.sprite);
                
                break;
            
            case "coin":
                
                ItemImg.sprite = ResMgr.Instance.load<Sprite>("Sprite/Money");
                
                break;
        }
        
        //更新要完成的数量文本
        countText.text = currentDemandData.itemNum.ToString();
        //更新数量信息 数据可能会一直在变化 根据实际完成的数量发生变化
        UpdateNumData();
        //更新任务的状态信息
        UpdateCheckIcon();
    }

    /// <summary>
    /// 更新任务完成的状态
    /// </summary>
    private void UpdateCheckIcon()
    {
        isFinishIcon.enabled = taskItemData.isFinished;
        /*if (taskItemData.isFinished)
        {
            isFinishIcon.enabled = true;
        }
        else
        {
            isFinishIcon.enabled = false;
        }*/
    }

    /// <summary>
    /// 当建造物 金币 等等信息发生变化时会执行这个方法
    /// </summary>
    private void UpdateNumData()
    {
        if (currentDemandData == null)
        {
            Debug.LogError("currentDemandData为空!");
            return;
        }
        // 去除可能的空格
        string itemType = currentDemandData.itemType?.Trim();

        //判断监听的物体的类型  金币  建造物  其他
        switch (itemType)
        {
            case "building":

                //拿到对应建造物的数量
                BuildItemData build = GameManager.instance.buildItemDict[currentDemandData.itemId];


                // 检查buildItemDict是否包含这个key
                if (!GameManager.instance.buildItemDict.ContainsKey(currentDemandData.itemId))
                {
                    Debug.LogError($"buildItemDict中不包含key: {currentDemandData.itemId}");
                    return;
                }


                //通过建造物的id找到当前建造的字典
                int num = 0;
                if (!BuildController.Instance.currentBuildingDict.ContainsKey(build.id) || BuildController.Instance.currentBuildingDict[build.id].Count == 0)
                {
                    num = 0;
                }
                else
                {
                    //拿到建造物的数量
                    num = BuildController.Instance.currentBuildingDict[build.id].Count;
                }
                //更新文本信息
                currentCountText.text = num.ToString();
                currentDemandData.currentNum = num;
                GameManager.instance.GetNumDemandData(currentDemandData.id).currentNum = num;
                //任务要求达成 任务还没有结束
                if (num >= currentDemandData.itemNum && !taskItemData.isEnd)
                {
                    //触发任务结束的事件
                    EventCenter.Instance.EventTrigger<taskItem>(GameEvent.任务结束事件,this);
                }
                
                break;
            
            case "coin":
                
                currentCountText.text = GameManager.instance.CurrPlayerData.Coin.ToString();
                currentDemandData.currentNum = (int)GameManager.instance.CurrPlayerData.Coin;
                GameManager.instance.GetNumDemandData(currentDemandData.id).currentNum = currentDemandData.currentNum;
                if (currentDemandData.currentNum >= currentDemandData.itemNum && !taskItemData.isEnd)
                {
                    //任务结束事件
                    EventCenter.Instance.EventTrigger<taskItem>(GameEvent.任务结束事件,this);
                }
                
                break;

            default:
                Debug.LogError($"未知的物品类型: '{itemType}'");
                break;
        }
    }
}
