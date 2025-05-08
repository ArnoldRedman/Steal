using Newtonsoft.Json;
using System.Collections.Generic;

public class PlayerData
{
    private int coin;

    public int Coin//属性 get set(设置值的时候会执行方法)
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
            EventCenter.Instance.EventTrigger(GameEvent.金币发生改变);
        }
    }
    public string name;
}

/// <summary>
/// 土地信息
/// </summary>
public class GroundPropertyData
{
    //0 未购买；1 已购买；2已建造
    public int State;
    public string GroundName;
    public int Price = 200;
    public bool isShowInitPrefab;//建造了东西就要隐藏花花草草
}

/// <summary>
/// 建造模块的数据对象，在表格中一一对应
/// </summary>
public class BuildItemData
{
    public string id;//建造id
    public string type;//建造类型
    public string name;//建造物名字
    public string prefab;//建造物预制体
    public int price;//建造价格
    public int keepCost;//每日消耗（维护价格）
    public int ripeningTime;//成熟时间
    [JsonConverter(typeof(ArrayToDictionaryConverter<string, int>))]
    public Dictionary<string, int> product;//所有物品产出的字典
    public int firstGrowTime;//初次生长时间
    public string sprite;//UI图片地址
    public string decription;//建造物描述
    public int jieduan2;//到达阶段2时间
    public int jieduan3;//到达阶段3时间    
}

/// <summary>
/// 产出物品信息对象
/// </summary>
public class ProductItemData
{
    public string id; //物品id 
    public string name; //物品名字
    public string unit; //产出物品的单位 克，个，块，束.....
    public string description; //物品描述
    public string sprite; //物品的图片名字  
    public int price; //物品的价值
}

/// <summary>
/// 背包数据对象 
/// </summary>
public class KnapsackData
{
    //物品的id 物品的数量 
    public Dictionary<string, int> productDict;
    //装备 装备的id 装备的等级  
}

/// <summary>
/// 消耗物品的数据对象
/// </summary>
public class XiaohaoItemData
{
    public string id;//lubanconfig要求的主键id
    public string buildId;//建造物id
    public string productId;//产出物品id
    [JsonConverter(typeof(ArrayToDictionaryConverter<string,int>))]
    public Dictionary<string, int> XiaohaoDict;//产出物品每天消耗的物品
}