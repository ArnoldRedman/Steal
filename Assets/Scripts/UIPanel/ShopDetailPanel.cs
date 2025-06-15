using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;   
/// <summary>
/// 商店详情面板 
/// </summary>
public class ShopDetailPanel : BasePanel
{
    [Header("拆除按钮")]
    public Button chaichuBtn;
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
        EventCenter.Instance.AddEventListener(GameEvent.拆除建造物,chaichu);
        EventCenter.Instance.AddEventListener(GameEvent.日期每日更新变化,updateEveryDayData);
    }

    private void Init()
    {
        UpgradeBtn.onClick.AddListener(upgradeShopPrice);
        BuyBtn.onClick.AddListener(openBuyPanel);
        currentShopbuilding = BuildController.Instance.currentGround.transform.Find("Building").GetComponent<Shop>();
        updateEveryDayData();
        updateRateData();
    }
/// <summary>
/// 打开购买的商品的列表 初始化商品列表的信息  商品的信息，商品购买的最大值，商品是否能被购买的信息 通过当前游戏等级来判断的  默认游戏从0等级开始 
/// </summary>
    private void openBuyPanel()
    {
        SellItemPanel.SetActive(false);
        BuyItemPanel.gameObject.SetActive(true);
        BuyPanelInit();
    }
/// <summary>
/// 商品列表初始化  
/// </summary>
    private void BuyPanelInit()
    {
        for (int i = 0; i < BuyItemCotent.transform.childCount; i++)
        {
            Destroy(BuyItemCotent.transform.GetChild(i).gameObject);
        }
        if (GameManager.Instance.merchantingList.Count == 0) return;
        //遍历所有的商品信息  
        foreach (MerchantingData data  in GameManager.Instance.merchantingList)
        {
            GameObject obj = ResMgr.Instance.load<GameObject>("UI/BuyItem",BuyItemCotent.transform);
            obj.GetComponent<BuyItem>().updateBuyItem(data);
        }
    }

    /// <summary>
/// 升级店铺 
/// </summary>
    private void upgradeShopPrice()
    {
        //更改shop的信息  
        if (currentShopbuilding.level>=currentShopbuilding.maxLevel)
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("已到最高等级");
            return;
        }else if (GameManager.Instance.playerData.Coin<currentShopbuilding.upgradePrice)
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("金币不足,无法升级");
            return;
        }
        //更新shop的信息  
        currentShopbuilding.level++;
        currentShopbuilding.priceRate += 0.1f;
        GameManager.Instance.playerData.Coin -= currentShopbuilding.upgradePrice;
        //更新价格比例的信息
        updateRateData();
    }

    /// <summary>
/// 更新价格比例的信息
/// </summary>
    private void updateRateData()
    {
        priceRate=currentShopbuilding.priceRate;    
        priceRateTxt.text = priceRate.ToString("0%");
        levelTxt.text = $"LV.{currentShopbuilding.level}";
    }

    /// <summary>
/// 更新状态信息 
/// </summary>
    private void updateStateData()
    {
        if (currentShopbuilding.isMoneyEnough())
        {
            moneyCheckTxt.text = "金币充足";
            moneyCheckTxt.color = Color.black;
        }
        else
        {
            moneyCheckTxt.text="金币不足,商店无法运作";
            moneyCheckTxt.color = Color.red;
        }
    }

    /// <summary>
/// 更新收支的信息  
/// </summary>
    private void updateShouzhi()
    {
        weihuTxt.text=currentShopbuilding.weihuPrice.ToString();    
        shouyiTxt.text=currentShopbuilding.earnings.ToString(); 
        totalIncomeTxt.text=currentShopbuilding.Incom.ToString();   
    }
/// <summary>
/// 每日要更新的内容
/// </summary>
    private void updateEveryDayData()
    {
        updateStateData();
        updateShouzhi();
    }

    private void OnDisable()
    {
        StopCoroutine(DelayedLayoutUpdate());
        UpgradeBtn.onClick.RemoveAllListeners();
        BuyBtn.onClick.RemoveAllListeners();
        chaichuBtn.onClick.RemoveAllListeners();    
        EventCenter.Instance.RemoveEventListener(GameEvent.日期每日更新变化,updateEveryDayData);
        EventCenter.Instance.RemoveEventListener(GameEvent.拆除建造物,chaichu);
    }
    
    /// <summary>
    /// 拆除建造物  
    /// </summary>
    private void chaichu()
    {
        UIManager.Instance.closePanel<ShopDetailPanel>();
    }
}
