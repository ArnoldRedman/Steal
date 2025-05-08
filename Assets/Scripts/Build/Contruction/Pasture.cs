using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承牧场基类
public class Pasture : BuildItemBase
{
    private void Start()
    {
        Init();
        EventCenter.Instance.AddEventListener(GameEvent.日期时间每日更新事件,TurnDay);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.日期时间每日更新事件, TurnDay);
    }
}
