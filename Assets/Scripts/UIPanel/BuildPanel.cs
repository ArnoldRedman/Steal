using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildPanel : BasePanel
{
    public GameObject buildTypeList;
    public GameObject buildItemList;
    void Start()
    {
        buildDataInit();
        EventCenter.Instance.AddEventListener(GameEvent.建造物品成功,Hide);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.建造物品成功,Hide);
    }

    public void buildDataInit()
    {
        for (int i = 0; i < buildTypeList.transform.childCount; i++)
        {
            Button btn=buildTypeList.transform.GetChild(i).gameObject.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                openCurrentBuildItemList(btn.gameObject.name);
            });
            string typeName=buildTypeList.transform.GetChild(i).name;//拿到建造类型名字
            //克隆出n个建造的panel  
            GameObject panel= ResMgr.Instance.load<GameObject>("UI/UIPanel/BuildItemsPanel");
            panel.name=typeName;
            panel.transform.SetParent(buildItemList.transform);
            panel.transform.localPosition = Vector3.one;
            panel.transform.localScale = Vector3.one;
            
        }
    }

    private void openCurrentBuildItemList(string typeName)
    {
        //其他的关闭 显示当前的建造的列表 
        for (int i = 0; i < buildItemList.transform.childCount; i++)
        {

            if (buildItemList.transform.GetChild(i).name != typeName)
            {
                buildItemList.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                buildItemList.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
