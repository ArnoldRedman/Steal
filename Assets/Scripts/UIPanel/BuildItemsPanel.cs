using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItemsPanel : BasePanel
{
    //容器
    public GameObject content;

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        EventCenter.Instance.AddEventListener(GameEvent.建造物品成功, Hide);
        updateData();
        Hide();
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.建造物品成功, Hide);
    }

    public void updateData()
    {
        string type = this.name; //名字和类型一样 
        foreach (BuildItemData item in GameManager.Instance.buildItemDict.Values)
        {
            if (item.type == type)
            {
                //克隆我们的builditem预制体到我们的content下面 
                GameObject newItem = ResMgr.Instance.load<GameObject>("UI/BuildItem", content.transform);
                newItem.GetComponent<BuildItem>().UpdateData(item);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}