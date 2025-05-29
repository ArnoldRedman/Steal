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
        
    }
}
