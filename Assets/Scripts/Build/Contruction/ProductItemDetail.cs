using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductItemDetail : MonoBehaviour
{
    public Image icon;
    public Text name;
    public Text num;
    public Text value;
    public Button chooseBtn;
    private BuildItemBase currentBuildItem;
    public string productId;
    void Start()
    {
        chooseBtn.onClick.AddListener(ChooseProduct);
    }

    private void OnDestroy()
    {
        chooseBtn.onClick.RemoveListener(ChooseProduct);
    }

    /// <summary>
    /// 选中要产出的物品所执行的方法
    /// </summary>
    private void ChooseProduct()//更改对应建造物的id   
    {
        currentBuildItem.productItemId = productId;  
        currentBuildItem.UpdateXiaohaoDict(currentBuildItem.buildid,productId);
        //触发id变化的事件 执行ui相关的更新
        EventCenter.Instance.EventTrigger(GameEvent.产出物品id变化);
    }

    public void UpdateData(ProductItemData productItemData,int productNum,BuildItemBase builitem)
    {
        currentBuildItem = builitem;
        productId = productItemData.id;
        icon.sprite = ResMgr.Instance.load<Sprite>("Sprite/" + productItemData.sprite);
        name.text = productItemData.name;
        num.text=productNum.ToString();
        value.text=productItemData.price.ToString();
    }

}
