using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 产出物品UI项
/// </summary>
public class ProductItem : MonoBehaviour
{
    public Image icon;//产出物品的图标
    public Text name;//产出物品的名字
    public Text num;//产出物品的数量

    /// <summary>
    /// 更新面板数据
    /// </summary>
    /// <param name="id">产出物品的id</param>
    /// <param name="productNum">产出数量</param>
    public void UpdateData(string id,int productNum)
    {
        icon.sprite = ResMgr.Instance.load<Sprite>("Sprite/"+GameManager.instance.productItemDict[id].sprite);
        name.text = GameManager.instance.productItemDict[id].name;
        num.text =productNum.ToString();
    }

}
