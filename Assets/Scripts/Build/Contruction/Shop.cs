using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : BuildItemBase
{
    //价格比例  
    [HideInInspector]
    public float priceRate;
    //商店最大的等级 
    [HideInInspector]
    public int maxLevel;
    //当前商店的等级 
    [HideInInspector]
    public int level;
    //升级的价格 
    [HideInInspector]
    public int upgradePrice;
    //总收入 
    [HideInInspector]
    public float Incom;
    //每日产出的金币 
    [HideInInspector]
    public int chanchuCoin;
    //收益
    [HideInInspector]
    public float earnings;
    //商品的字典  
    public Dictionary<string, int> merchantingDict;

    public virtual void Start()
    {
        Init();
        EventCenter.Instance.AddEventListener(GameEvent.日期时间每日更新事件,TurnDay);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.日期时间每日更新事件,TurnDay);
    }

    /// <summary>
    /// 因为部分信息不用初始化 所以要重写
    /// </summary>
    public override void Init()
    {
        //拿到当前建造物的信息
        BuildItemData buildItemData = GameManager.instance.buildItemDict[buildid];
        level = 1;
        priceRate = 1;
        upgradePrice = 10000;
        maxLevel = 5;
        weihuPrice = buildItemData.keepCost;
        chanchuCoin = 900;
        earnings = (chanchuCoin - weihuPrice) * priceRate;
        Incom += earnings;
        buildType = buildItemData.type;
        name = buildItemData.name;
        isMoneyEnough = IsMoneyEnough();
        canProduct = isMoneyEnough;
    }

    public override void TurnDay()
    {
        isMoneyEnough = IsMoneyEnough();
        canProduct = isMoneyEnough;//更新商店运作状态
        if (!canProduct)
        {
            return;
        }
        earnings = (chanchuCoin - weihuPrice) * priceRate;//更新每日收益
        Incom += earnings;//更新总收入
        GameManager.instance.CurrPlayerData.Coin += earnings;//更新玩家金币值
    }
}
