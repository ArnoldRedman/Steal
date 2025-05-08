using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 农场基类  
/// </summary>
public class HayFarm : BuildItemBase
{
    //1,2,3阶段建造模型 这里最好写在这里 因为其他非农场的建造物不一定有三个阶段 
    protected GameObject jieduan1Obj;
    protected GameObject jieduan2Obj;
    protected GameObject jieduan3Obj;

    public virtual void Start()
    {
        jieduan1Obj = transform.Find("1").gameObject;
        jieduan2Obj = transform.Find("2").gameObject;
        jieduan3Obj = transform.Find("3").gameObject;
        Init();
        Danqianjieduan(1);
        EventCenter.Instance.AddEventListener(GameEvent.日期时间每日更新事件, TurnDay);
    }

    /// <summary>
    /// 重写TurnDay方法
    /// </summary>
    public override void TurnDay()
    {
        shouhuoTime--;
        //判断当前是否过了生长周期
        if (!isShengzhangqi)
        {
            if (currentProductTime >= shengzhangzhouqi)
            {
                isShengzhangqi = true;
                currentProductTime = 0;
                return;
            }

            if (currentProductTime >= jieduan2) Danqianjieduan(2);
            currentProductTime++;
            return;
        }

        //切换阶段模型  过了生长周期 
        if (currentProductTime >= jieduan2 && currentProductTime < jieduan3)
        {
            Danqianjieduan(2);
        }
        else if (currentProductTime >= jieduan3)
        {
            Danqianjieduan(3);
        }

        //判断是否到产出时间  
        currentProductTime++; //当前生产周期
        if (currentProductTime >= ripeningTime)
        {
            shouhuoTime = ripeningTime;
            currentProductTime = 0;
            //到了生产周期时间，在背包中添加生产的物品  
            //更新背包数据
            GameManager.instance.knapsack.productDict[productItemId] +=
                GameManager.instance.buildItemDict[buildid].product[productItemId];
            //背包数据变化 触发背包数据改变的事件
            EventCenter.Instance.EventTrigger(GameEvent.背包数据变化);
        }
    }

    /// <summary>
    /// 切换当前阶段的方法 
    /// </summary>
    /// <param name="i"></param>
    private void Danqianjieduan(int jieduan)
    {
        switch (jieduan)
        {
            case 1:
                jieduan1Obj.SetActive(true);
                jieduan2Obj.SetActive(false);
                jieduan3Obj.SetActive(false);
                break;
            case 2:
                jieduan1Obj.SetActive(false);
                jieduan2Obj.SetActive(true);
                jieduan3Obj.SetActive(false);
                break;
            case 3:
                jieduan1Obj.SetActive(false);
                jieduan2Obj.SetActive(false);
                jieduan3Obj.SetActive(true);
                break;
        }
    }

    public virtual void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.日期时间每日更新事件, TurnDay);
    }
}