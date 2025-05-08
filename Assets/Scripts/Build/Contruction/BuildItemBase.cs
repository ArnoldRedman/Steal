using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 所有物品的建造基类
/// </summary>
public class BuildItemBase : MonoBehaviour
{
    //当前产出物品的id  
    [HideInInspector] public string productItemId;

    //成熟时间  生产周期 
    [HideInInspector] public int ripeningTime;

    //每日维护价格  
    [HideInInspector] public int weihuPrice;

    //当前生产周期 记录当前生产周期  0~产出时间
    [HideInInspector] public int currentProductTime;
    [Header("建造物id")] public string buildid; //建造物的id 
    [HideInInspector]
    public bool isShengzhangqi; //是否在生长期
    [HideInInspector]
    public int shengzhangzhouqi; //生长周期 初次生长期
    protected int dangqianjieduan = 1;//当前阶段
    protected int jieduan2;//到达阶段2的时间
    protected int jieduan3;//到达阶段3的时间
    [HideInInspector] public int totalTime;//当前建造物出现的总时间
    [HideInInspector] public int shouhuoTime;//收获时间

    public virtual void Init()//初始化建造物的信息
    {
        BuildItemData buildItemData = GameManager.instance.buildItemDict[buildid];//拿到当前对应id的建造物信息
        productItemId = buildItemData.product.Keys.First();
        print(productItemId);
        ripeningTime = buildItemData.ripeningTime;//物品产出时间
        currentProductTime = 0;
        weihuPrice = buildItemData.keepCost;//维护成本 每日消费
        shengzhangzhouqi = buildItemData.firstGrowTime;
        isShengzhangqi = false;
        jieduan2 = buildItemData.jieduan2;
        jieduan3 = buildItemData.jieduan3;

        shouhuoTime = ripeningTime + shengzhangzhouqi;//第一次的收获时间 = 生长周期 + 成熟时间
    }

    public virtual void TurnDay()
    {
        totalTime++;

        shouhuoTime--;

        if (!isShengzhangqi) //判断当前是否过了生长周期 只有过了生长期 才能正常的去产出物品
        {
            if (currentProductTime >= shengzhangzhouqi)//判断生长周期的时间是不是到了
            {
                isShengzhangqi = true;//已经到了生长期
                currentProductTime = 0;
                return;
            }
            currentProductTime++;
            return;//没过生长期 直接return 下面关于物品产出的方法就不会执行了
        }
        //计算产出
        currentProductTime++; //当前生产周期
        if (currentProductTime >= ripeningTime)
        {
            shouhuoTime = ripeningTime;//重新将收获时间变为成熟时间
            currentProductTime = 0;
            //到了生产周期时间，在背包中添加生产的物品  
                //更新背包数据
                GameManager.instance.knapsack.productDict[productItemId] +=
                    GameManager.instance.buildItemDict[buildid].product[productItemId];
                print(GameManager.instance.buildItemDict[buildid].product[productItemId]);
            

            //背包数据变化
            EventCenter.Instance.EventTrigger(GameEvent.背包数据变化);
        }
    }
}