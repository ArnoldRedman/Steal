using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包面板UI 
/// </summary>
public class KnapsackPanel : BasePanel
{
    //当前背包显示的所有物品
    private Dictionary<string, ProductItem> currentShowProductItems = new Dictionary<string, ProductItem>();

    public GameObject content;//放置物品项的容器
    //每次打开面板的时候从背包数据中获取当前最新的信息
    private void OnEnable()
    {

    }

    void Start()
    {
        EventCenter.Instance.AddEventListener(GameEvent.背包数据变化, UpdateData);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.背包数据变化, UpdateData);
    }

    //更新背包面板数据
    public void UpdateData()
    {
        Dictionary<string, int> productDict = GameManager.instance.knapsack.productDict;//创建一个字典 背包的数据
        foreach (var id in productDict.Keys)
        {
            if (productDict[id] > 0)//数量大于0就显示产出物品
            {
                if (currentShowProductItems.ContainsKey(id))//如果当前数据不为0 下次就增加数据
                {
                    //更新UI数据 把最新的物品的产出的数量传过去
                    currentShowProductItems[id].UpdateData(id, productDict[id]);
                }
                else//当前数据为0 背包里没有这个东西 克隆一个新的放到content下面   
                {
                    GameObject newProductItem = ResMgr.Instance.load<GameObject>("UI/ProductItem", content.transform);
                    ProductItem productItem = newProductItem.GetComponent<ProductItem>();//拿到ProductItem脚本
                    productItem.UpdateData(id, productDict[id]);
                    currentShowProductItems.Add(id, productItem);//添加到已显示的面板字典中
                }
            }
            else//为0则不显示产出物品 销毁   
            {
                if (currentShowProductItems.ContainsKey(id))
                {
                    DestroyImmediate(currentShowProductItems[id].gameObject);
                    currentShowProductItems.Remove(id);
                }
            }
        }
    }
}