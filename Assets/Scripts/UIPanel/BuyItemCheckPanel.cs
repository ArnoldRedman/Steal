using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemCheckPanel : BasePanel
{
    public Text name;
    public InputField countInput;
    public Button jianBtn;
    public Button jiaBtn;
    public Text moneyText;
    public Button checkBtn;
    private int maxCount;
    private int currentCount;
    public MerchantingData currentMerchantingData;//当前商品的信息 
    public ProductItemData productItemData;//商品的详细信息
    private bool isMoneyEnough;
    private bool isShowBuyBtn;
    private void OnEnable()
    {
        countInput.onValueChanged.AddListener(countChange);
        jianBtn.onClick.AddListener(jian);
        jiaBtn.onClick.AddListener(jia);
        checkBtn.onClick.AddListener(buyCheck);
        
    }
/// <summary>
/// 购买核对  
/// </summary>
    private void buyCheck()
    {
        if (GameManager.Instance.playerData.Coin>=float.Parse(moneyText.text))
        {
            GameManager.Instance.playerData.Coin -= float.Parse(moneyText.text);            
            UIManager.Instance.closePanel<BuyItemCheckPanel>();
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("购买成功");
            GameManager.Instance.currentKnapsack.productDict[currentMerchantingData.productid] += currentCount;
            EventCenter.Instance.EventTrigger(GameEvent.背包数据变化);

        }
        else
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("当前金币不足，请重新选择");
        }
    }

    private void countChange(string newCount)
    {
        if (int.Parse(newCount)<=0)
        {
            countInput.text = "0";
            currentCount = 0;
        }else if (int.Parse(newCount)>=maxCount)
        {
            currentCount = maxCount;
            countInput.text=maxCount.ToString();     
        }
        UpdateMoney();
    }

    public void updateData(MerchantingData data)
    {
        currentMerchantingData = data;
        productItemData = GameManager.Instance.productItemDict[currentMerchantingData.productid];
        name.text=productItemData.name;
        maxCount=currentMerchantingData.maxCount;
        currentCount = 0;
        countInput.text=currentCount.ToString();    
        UpdateMoney();

    }

    private void jia()
    {
        if (currentCount>=maxCount)
        {
            return;
        }
        currentCount++;
        countInput.text=currentCount.ToString();
        UpdateMoney();
   
    }

    private void jian()
    {
        if (currentCount<=0)
        {
            return;
        }
        currentCount--;
        countInput.text=currentCount.ToString();
        UpdateMoney();
      
    }

    public void UpdateShowBuyBtn()
    {
        if (isShowBuyBtn)
        {
            checkBtn.gameObject.SetActive(true);
        }
        else
        {
            checkBtn.gameObject.SetActive(false);
        }
    }
    
    public void UpdateMoney()
    {
         int currentPrice = currentCount * productItemData.price;
         moneyText.text=currentPrice.ToString();
         if (GameManager.Instance.playerData.Coin>=currentPrice)
         {
             isShowBuyBtn=true;
             moneyText.color=Color.black;
         }
         else
         {
             isShowBuyBtn=false;
             moneyText.color=Color.red;
         }
         UpdateShowBuyBtn();
    }
    private void OnDisable()
    {
        jianBtn.onClick.RemoveAllListeners();
        jiaBtn.onClick.RemoveAllListeners();
        countInput.onValueChanged.RemoveAllListeners();
        checkBtn.onClick.RemoveAllListeners();
    }
}
