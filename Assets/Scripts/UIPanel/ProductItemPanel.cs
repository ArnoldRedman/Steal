using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 产出物品项的面板   产出多少项   只要选择了新的物品的信息更新对应建造物产出物品的id 
/// </summary>
public class ProductItemPanel : BasePanel
{
    public GameObject content;//放置物品的地方 
    /// <summary>
    /// 每次打开要清除上一次产出的物品项  显示最新的物品项   有多少产出物品 产出的物品的信息   物品的产量   
    /// </summary>
    private void OnEnable()
    {
        
    }

    public void UpdateData(int num,ProductItemData productItemData,int productNum)
    {
        //先清除原来的物品项  
        for (int i = 0; i <content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        //克隆新的物品项 
        for (int i = 0; i <num; i++)
        {
            
        }
        
    }
    private void OnDisable()
    {
        
    }
}
