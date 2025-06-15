using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropPanel : BasePanel
{
    [Header("金币")]
    public Text CoinText;
    [Header("年")]
    public Text YearText;
    [Header("月")]
    public Text MonthText;
    [Header("日")]
    public Text DayText;
    [Header("季节")]
    public Text SeasonText;
    [Header("背包按钮")]
    public Button KnapsackBtn;
    void Start()
    {
        updateCoin();
        EventCenter.Instance.AddEventListener(GameEvent.日期每日更新变化,updateTime);
        EventCenter.Instance.AddEventListener(GameEvent.金币变化,updateCoin);
        KnapsackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.openPanel<KnapsackPanel>();
        });
    }

    private void updateCoin()
    {
        CoinText.text= GameManager.Instance.playerData.Coin.ToString("0000");
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.日期每日更新变化,updateTime);
        EventCenter.Instance.RemoveEventListener(GameEvent.金币变化,updateCoin);
    }

    //更新时间 
    private void updateTime()
    {
       //拿到当前时间戳 
       long ticks = GameManager.Instance.gameTimeController.ticks;
       //将时间戳转成日期对象 
       DateTimeOffset now = DateTimeOffset.FromUnixTimeSeconds(ticks);
       YearText.text = now.Year.ToString();
       MonthText.text = now.Month.ToString();
       DayText.text = now.Day.ToString();
       switch (now.Month)
       {
           case 3:case 4:case 5:
               SeasonText.text = "春天";
               break;
           case 6:case 7:case 8:
               SeasonText.text="夏天";
               break;
           case 9:case 10:case 11:
               SeasonText.text = "秋天";
               break;
           case 12:case 1:case 2:
               SeasonText.text = "冬天";
               break;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
