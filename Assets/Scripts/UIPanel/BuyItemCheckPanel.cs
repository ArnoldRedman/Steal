using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemCheckPanel : BasePanel
{
    [Header("商品的名字")]
    public Text name;
    [Header("购买数量的输入框")]
    public InputField countInput;
    [Header("减少按钮")]
    public Button reduceBtn;
    [Header("增加按钮")]
    public Button addBtn;
    [Header("消耗的金币值")]
    public Text moneyText;
    [Header("确认购买按钮")]
    public Button checkBtn;
    //最大的数量
    private int maxCount;
    //当前数量
    private int currCount;
    //当前商品的信息
    public MerchantingData currMerchantingData;
    //商品的详细信息
    public ProductItemData productItemData;
    //金币是否足够
    private bool isMoneyEnough;
    //当金币不够的时候，金币的颜色显示红色，同时购买按钮不显示
    private bool isShowButBtn;

    private void OnEnable()
    {
        countInput.text = 0.ToString();
        countInput.onValueChanged.AddListener(CountChange);
        reduceBtn.onClick.AddListener(Reduce);
        addBtn.onClick.AddListener(Add);
        checkBtn.onClick.AddListener(BuyCheck);
    }

    public void UpdateData(MerchantingData data)
    {
        currMerchantingData = data;
        productItemData = GameManager.instance.productItemDict[currMerchantingData.productid];
        name.text = productItemData.name;
        maxCount = currMerchantingData.maxCount;
        currCount = 0;
        countInput.text = currCount.ToString();
        UpdateMoney();
    }

    /// <summary>
    /// 确认按钮的购买判断
    /// </summary>
    private void BuyCheck()
    {
        if (currCount == 0)
        {
            return;
        }
        if (GameManager.instance.CurrPlayerData.Coin >= float.Parse(moneyText.text.Trim()))
        {
            GameManager.instance.CurrPlayerData.Coin -= float.Parse(moneyText.text);
            UIManager.Instance.closePanel<BuyItemCheckPanel>();
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("购买成功");
            //往背包里添加购买的物品
            GameManager.instance.knapsack.productDict[currMerchantingData.productid] += currCount;
            //背包物品发生变化，触发数据变化事件
            EventCenter.Instance.EventTrigger(GameEvent.背包数据变化);
        }
        else
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("当前金币不足");
        }
    }

    private void Add()
    {
        if (currCount >= maxCount)
        {
            return;
        }

        currCount++;
        countInput.text = currCount.ToString();
        UpdateMoney();
    }

    private void Reduce()
    {
        if (currCount <= 0)
        {
            return;
        }
        currCount--;
        countInput.text = currCount.ToString();
        //更新消耗的金币
        UpdateMoney();
    }

    /// <summary>
    /// 更新金币信息  
    /// </summary>
    private void UpdateMoney()
    {
        int currPrice = currCount * productItemData.price;
        moneyText.text = currPrice.ToString();
        //判断金币是否足够
        if (GameManager.instance.CurrPlayerData.Coin>=currPrice)
        {
            isShowButBtn = true;
            moneyText.color = Color.black;
        }
        else
        {
            isShowButBtn = false;
            moneyText.color = Color.red;
        }

        UpdateShowBuyBtn();
    }

    /// <summary>
    /// 更新购买按钮状态
    /// </summary>
    private void UpdateShowBuyBtn()
    {
        checkBtn.gameObject.SetActive(isShowButBtn);
    }

    /// <summary>
    /// 输入框内容发生变化时执行的方法
    /// </summary>
    public void CountChange(string newValue)
    {
        string countStr = newValue.Trim();//去掉空格
        int count = int.Parse(countStr);
        currCount = count;
        if (count < 0)
        {
            countInput.text = 0.ToString();
            currCount = 0;
        }
        else if (count >= maxCount)
        {
            countInput.text = maxCount.ToString();
            currCount = maxCount;
        }
        UpdateMoney();
    }
}
