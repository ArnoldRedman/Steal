using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackPanel : BasePanel
{
    public Dictionary<string, ProductItem> currentShowProductDict = new Dictionary<string, ProductItem>();
    public GameObject content; //放背包数据项

    private void OnEnable()
    {
        UpdateData();
    }

    void Start()
    {
        EventCenter.Instance.AddEventListener(GameEvent.背包数据变化,UpdateData);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.背包数据变化,UpdateData);
    }

    /// <summary>
    /// 背包信息更新的方法  背包中的物品数量为0就不显示 大于0显示 
    /// </summary>
    public void UpdateData()
    {
        //背包中所有物品的数量信息
        Dictionary<string, int> productDict = GameManager.Instance.currentKnapsack.productDict;
        //遍历背包中的数据
        foreach (var id in productDict.Keys)
        {
            if (productDict[id] > 0) //显示
            {
                if (currentShowProductDict.ContainsKey(id))
                {
                    currentShowProductDict[id].UpdateData(id, productDict[id]);
                }
                else //第一次显示  那就克隆一个新到content下面 
                {
                    GameObject newobj = ResMgr.Instance.load<GameObject>("UI/ProductItem",content.transform);
                    ProductItem productItem = newobj.GetComponent<ProductItem>();
                    productItem.UpdateData(id, productDict[id]);
                  
                    //添加到字典中 
                    currentShowProductDict.Add(id, productItem);
                }
            }
            else //不显示 
            {
                //销毁掉 
                if (currentShowProductDict.ContainsKey(id))
                {
                    ProductItem obj = currentShowProductDict[id];//先存起来
                    currentShowProductDict.Remove(id);//从字典中移除 
                    Destroy(obj.gameObject);//销毁 
                }
            }
        }
    }

    void Update()
    {
    }
}