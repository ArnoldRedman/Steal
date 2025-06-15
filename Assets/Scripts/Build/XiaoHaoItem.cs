using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiaoHaoItem : MonoBehaviour
{
    public Image xiaohaoIcon;
    public Text xiaohaoName;
    public Text xiaohaoNum;
    void Start()
    {
        
    }
/// <summary>
/// 
/// </summary>
/// <param name="productItemData">消耗物品的信息</param>
    public void updateData(ProductItemData productItemData,int num)
    {
        xiaohaoIcon.sprite=ResMgr.Instance.load<Sprite>($"Icon/{productItemData.sprite}");
        xiaohaoName.text = productItemData.name;
        xiaohaoNum.text = num.ToString();
    }
    void Update()
    {
        
    }
}
