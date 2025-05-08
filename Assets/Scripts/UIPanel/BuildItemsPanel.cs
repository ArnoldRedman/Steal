using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造物品的详细面板
/// </summary>
public class BuildItemsPanel : BasePanel
{
    public GameObject content;//容器

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        //建造物品成功后要隐藏掉
        EventCenter.Instance.AddEventListener(GameEvent.建造物品成功,Hide);
        UpdateData();
        //初始化 默认什么面板都不显示
        Hide();
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.建造物品成功,Hide);
    }

    private void UpdateData()
    {
        string type = name;//拿到当前面板要显示建造的类型
        foreach (var itemData in GameManager.instance.buildItemDict.Values)
        {
            if (itemData.type == type)
            {
                GameObject newItem = ResMgr.Instance.load<GameObject>("UI/BuildItem", content.transform);
                //刷新BuildItem面板中的数据
                newItem.GetComponent<BuildItem>().UpdateData(itemData);
            }
        }
    }
}
