using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商品详情的面板
/// </summary>
public class ShopDetailPanel : BasePanel
{
    [Header("标题名字文本")]
    public Text TileNameText;
    [Header("买入按钮")]
    public Button BuyBtn;
    [Header("卖出按钮")]
    public Button SellBtn;
    //价格比例 
    private float priceRate;
    [Header("价格比例文本")]
    public Text priceRateTxt;
    //商店等级
    private int level;
    [Header("等级文本")]
    public Text levelTxt;
    //升级的价格
    private int upgradePrice;
    [Header("升级按钮")]
    public Button UpgradeBtn;
    [Header("升级价格文本")]
    public Text UpgradePriceTxt;
    [Header("每升一次级上调的价格比例")]
    public Text PriceRateTxt;
    [Header("每天维护的成本")]
    public Text weihuTxt;
    [Header("商店每日收益文本")]
    public Text shouyiTxt;
    [Header("总收入")]
    public Text totalIncomeTxt;
    [Header("金币状态信息")]
    public Text moneyCheckTxt;
    [Header("购买的物品列表面板")]
    public GameObject BuyItemPanel;
    [Header("购买的物品容器")]
    public GameObject BuyItemCotent;
    [Header("卖出物品的列表面板")]
    public GameObject SellItemPanel;
    [HideInInspector]//当前的商店建造物
    public Shop currentShopbuilding;

    private void OnEnable()
    {
        StartCoroutine(DelayedLayoutUpdate());
        Init();
        EventCenter.Instance.AddEventListener(GameEvent.日期时间每日更新事件,UpdateEveryDay);
    }

    /// <summary>
    /// 面板详情初始化
    /// </summary>
    private void Init()
    {
        SellItemPanel.SetActive(false);
        BuyItemPanel.SetActive(false);
        BuyBtn.onClick.AddListener(OpenBuyPanel);
        //升级商店
        UpgradeBtn.onClick.AddListener(UpgradeShopPrice);
        currentShopbuilding = BuildController.Instance.currGround.GetComponentInChildren<Shop>();
        UpdateEveryDay();
    }

    /// <summary>
    /// 打开购买的窗口
    /// </summary>
    private void OpenBuyPanel()
    {
        SellItemPanel.SetActive(false);
        BuyItemPanel.SetActive(true);
        BuyPanelInit();
    }

    /// <summary>
    /// 初始化购买面板信息 克隆n项商品
    /// </summary>
    private void BuyPanelInit()
    {
        //先删除之前的商品信息
        for (int i = 0; i < BuyItemCotent.transform.childCount; i++)
        {
            Destroy(BuyItemCotent.transform.GetChild(i));
        }
        //再更新商品信息
        if (GameManager.instance.merchantingList.Count == 0)
        {
            return;
        }

        foreach (var item in GameManager.instance.merchantingList)
        {
            GameObject obj = ResMgr.Instance.load<GameObject>("UI/BuyItem", BuyItemCotent.transform);
            obj.GetComponent<BuyItem>().UpdateBuyItem(item);
        }
    }

    /// <summary>
    /// 升级商店
    /// </summary>
    private void UpgradeShopPrice()
    {
        if (currentShopbuilding.level >= currentShopbuilding.maxLevel)
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("当前已达最高等级");
            return;
        }
        else if(GameManager.instance.CurrPlayerData.Coin < currentShopbuilding.upgradePrice)
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("金币不足，无法升级");
            return;
        }
        //GameManager.instance.CurrPlayerData.GameLevel++;
        //更新等级
        currentShopbuilding.level++;
        currentShopbuilding.priceRate += 0.1f;
        GameManager.instance.CurrPlayerData.Coin -= currentShopbuilding.upgradePrice;
        //更新价格比例
        UpdateRateData();
    }

    /// <summary>
    /// 价格比例相关的信息
    /// </summary>
    private void UpdateRateData()
    {
        priceRate = currentShopbuilding.priceRate;
        priceRateTxt.text = priceRate.ToString("0%");
        levelTxt.text = $"LV.{currentShopbuilding.level.ToString()}";
    }


    /// <summary>
    /// 每日更新的方法
    /// </summary>
    public void UpdateEveryDay()
    {
        UpdateStateData();//状态信息
        UpdateShouzhi();//收治信息
    }

    /// <summary>
    /// 更新收治信息
    /// </summary>
    private void UpdateShouzhi()
    {
        weihuTxt.text = currentShopbuilding.weihuPrice.ToString();
        shouyiTxt.text = currentShopbuilding.earnings.ToString();
        totalIncomeTxt.text = currentShopbuilding.Incom.ToString();
    }

    /// <summary>
    /// 更新状态信息 判断金币状态
    /// </summary>
    private void UpdateStateData()
    {
        if (currentShopbuilding.isMoneyEnough)
        {
            moneyCheckTxt.text = "金币充足";
            moneyCheckTxt.color = Color.black;
        }
        else
        {
            moneyCheckTxt.text = "金币不足，商店无法运作";
            moneyCheckTxt.color = Color.red;
        }
    }

    private void OnDisable()
    { 
        StopCoroutine(DelayedLayoutUpdate());
        EventCenter.Instance.RemoveEventListener(GameEvent.日期时间每日更新事件,UpdateEveryDay);
    }
}
