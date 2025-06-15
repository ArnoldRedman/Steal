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
        chooseBtn.onClick.AddListener(chooseProduct);
    }

    private void OnDestroy()
    {
        chooseBtn.onClick.RemoveListener(chooseProduct);
    }

    private void chooseProduct()//更改对应建造物的id   
    {
        currentBuildItem.currentProductItemId = productId;  
        currentBuildItem.updateXiaoHaoDict(currentBuildItem.buildId,productId);
        EventCenter.Instance.EventTrigger(GameEvent.产出物品id变化);
    }

    public void UpdateData(ProductItemData productItemData,int productNum,BuildItemBase builitem)
    {
        currentBuildItem = builitem;
        productId = productItemData.id;
        icon.sprite = ResMgr.Instance.load<Sprite>("Icon/" + productItemData.sprite);
        name.text = productItemData.name;
        num.text=productNum.ToString();
        value.text=productItemData.price.ToString();
    }
    void Update()
    {
        
    }
}
