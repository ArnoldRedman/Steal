using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 玩家主要数据面板
/// </summary>
public class PlayerPropPanel : BasePanel
{
    public Text yearText;
    public Text monthText;
    public Text dayText;
    public Text seasonText;
    public Text moneyText;
    public Button knapsackBtn;//背包按钮

    private void Start()
    {
        UpdateTimeData();
        UpdateCoin();
        EventCenter.Instance.AddEventListener(GameEvent.日期时间每日更新事件,UpdateTimeData);
        EventCenter.Instance.AddEventListener(GameEvent.金币发生改变,UpdateCoin);

        knapsackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.openPanel<KnapsackPanel>();
        });
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.日期时间每日更新事件, UpdateTimeData);
        EventCenter.Instance.RemoveEventListener(GameEvent.金币发生改变, UpdateCoin);
    }

    private void UpdateCoin()
    {
        moneyText.text = GameManager.instance.CurrPlayerData.Coin.ToString();
    }

    private void UpdateTimeData()
    {
        //将时间戳转换为事件对象（内置方法）
        DateTimeOffset dateObj = DateTimeOffset.FromUnixTimeSeconds(GameManager.instance.timerController.ticks);
        yearText.text = dateObj.Year.ToString();
        monthText.text= dateObj.Month.ToString();
        dayText.text= dateObj.Day.ToString();
        switch (dateObj.Month)
        {
            case 3:
            case 4:
            case 5:
                seasonText.text = "春天";
                break;
            case 6:
            case 7:
            case 8:
                seasonText.text = "夏天";
                break;
            case 9:
            case 10:
            case 11:
                seasonText.text = "秋天";
                break;
            case 12:
            case 1:
            case 2:
                seasonText.text = "冬天";
                break;
        }
    }
}
