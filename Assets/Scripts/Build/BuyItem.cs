using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [Header("产品的图片")]
    public Image icon;
    [Header("产品的名字")]
    public Text name;
    [Header("可购买最大的数量")]
    public Text count;
    [Header("购买的按钮")]
    public Button buyBtn;
    [Header("等级限制的提示信息")]
    public Text desText;
    //商品信息
    private MerchantingData currMerchantingData;

    private void Start()
    {
        buyBtn.onClick.AddListener(OpenBuyItemCheckPanel);
        //打开商品购买的详情列表，有可能当前玩家升级，同时列表要更新
        EventCenter.Instance.AddEventListener(GameEvent.玩家等级发生变化,UpdateDes);
    }

    private void OpenBuyItemCheckPanel()
    {
        UIManager.Instance.closePanel<BuyItemCheckPanel>();
        UIManager.Instance.openPanel<BuyItemCheckPanel>().UpdateData(currMerchantingData);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.玩家等级发生变化,UpdateDes);
    }

    /// <summary>
    /// 更新商品信息
    /// </summary>
    /// <param name="data">传进来当前商店要显示的商品的信息</param>
    public void UpdateBuyItem(MerchantingData data)
    {
        currMerchantingData = data;
        ProductItemData productItemData = GameManager.instance.productItemDict[data.productid];
        icon.sprite = ResMgr.Instance.load<Sprite>("Sprite/"+productItemData.sprite);
        name.text = productItemData.name;
        count.text = data.maxCount.ToString();
        UpdateDes();
    }

    /// <summary>
    /// 等级限制的信息
    /// </summary>
    private void UpdateDes()
    {
        //当前游戏等级
        int gameLevel = currMerchantingData.gameLevel;
        //判断游戏等级是否大于商品中的等级
        if (GameManager.instance.CurrPlayerData.GameLevel >= gameLevel)
        {
            desText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            desText.transform.parent.gameObject.SetActive(true);
            desText.text = $"达到{gameLevel}级后解锁该商品";
        }
    }
}
