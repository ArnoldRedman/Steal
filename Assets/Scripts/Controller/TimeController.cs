using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 时间控制器
/// </summary>
public class TimeController
{
    //https://www.jyshare.com/front-end/852/?utm_source=heifan
    //DateTimeOffset.UtcNow.ToUnixTimeSeconds();可获取当前时间的时间戳
    public long ticks = 1722009600;//初始的时间戳
    public float timer;//记录当前周期
    public float dayTime = 0.2f;//每隔dayTime的时间间隔更新一次时间

    /// <summary>
    /// 更新时间的方法
    /// 多少dayTime更新一天
    /// </summary>
    public void UpdateTime()
    {
        timer += Time.deltaTime;
        if (timer >= dayTime)
        {
            ticks += 86400;//这是一天的时间戳
            timer = 0;
            //触发每日更新的事件
            EventCenter.Instance.EventTrigger(GameEvent.日期时间每日更新事件);
        }
    }
}
