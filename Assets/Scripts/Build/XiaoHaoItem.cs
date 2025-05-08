using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiaoHaoItem : BasePanel
{
    public Image xiaohaoIcon;
    public Text xiaohaoName;
    public Text xiaohaoNum;

    /// <summary>
    /// 更新消耗的信息
    /// </summary>
    /// <param name="xiaohaoData">物品的信息</param>
    /// <param name="num">消耗的数量</param>
    public void UpdateData(ProductItemData xiaohaoData,int num)
    {
        if (xiaohaoData == null)
        {
            return;
        }

        xiaohaoIcon.sprite = ResMgr.Instance.load<Sprite>("Sprite/" + xiaohaoData.sprite);
        xiaohaoName.text = xiaohaoData.name;
        xiaohaoNum.text = num.ToString();
    }
}
