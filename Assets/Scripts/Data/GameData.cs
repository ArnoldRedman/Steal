using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    private float coin;
    public float Coin//属性 get set(设置值的时候会执行方法)
    {
        get => coin;
        set
        {
            coin = value;
            EventCenter.Instance.EventTrigger(GameEvent.金币发生改变);
        }
    }

    private int gameLevel;

    public int GameLevel
    {
        get => gameLevel;
        set
        {
            gameLevel = value;
            EventCenter.Instance.EventTrigger(GameEvent.玩家等级发生变化);
        }
    }
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
    //建造物id id为0表示这个地方没有建造物
    public string buildId;
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

/// <summary>
/// 售卖商品信息数据对象
/// </summary>
public class MerchantingData
{
    public string productid;//产品的id  产品数据表中的id  
    public int maxCount;//购买的最大数量
    public int gameLevel;//当前售卖的物品在哪个等级可以进行售卖  游戏的等级 

}

/// <summary>
/// 对话数据信息对象
/// </summary>
public class DialogueItemData
{
    public string id;//对话id
    public string targetName;//对话目标id
    public string targetIcon;//对话目标的图片
    public string dialogueContent;//对话内容
    public string nextId;//下一个对话的id
    public List<string> optionList;//选项列表
    public string taskId;//任务id
}

/// <summary>
/// 任务基本数据信息
/// </summary>
public class TaskItemData
{
    public string id;//任务id
    public bool isStarted;//任务是否进行中 如果开始就要添加到对应任务UI中
    public bool isEnd;//任务是否完成，完成触发奖励
    public bool isFinished;//触发奖励后显示任务已完成
    [JsonConverter(typeof(ArrayToDictionaryConverter<string,string>))]
    public Dictionary<string, string> demandDict;//需求字典，键为需求类型 值为需求id
    public string type;//任务需求类型 主线任务还是支线任务
    public int level;//该任务所处的游戏等级
}

/// <summary>
/// 判断数据需求量的信息
/// </summary>
public class NumDemandData
{
    public string id;//需求id
    public string itemId;//物品id
    public int currentNum;//当前完成数量
    public int itemNum;//需要完成的物品数量
    public string itemType;//物品类型
    public string descripe;//物品描述
    public string description => descripe;
}

/// <summary>
/// 普通建造物信息
/// </summary>
public class SampleBuildingData
{
    public int dangqianjieduan;//当前阶段
    public int currentTime;//当前时间
    public bool isOverShengzhangqi;//是否过了生长期
    public int shouhuoTime;//收获时间
    public bool IsMaterialEnough;//材料是否充足
    public bool canProduct;//能否建造
    public string currentProductItemId;//当前产出物品id
    public bool isAdult;//是否成年
    public string groundName;//所在土地的名字
}

/// <summary>
/// 商店建造物存档信息
/// </summary>
public class ShopBuildingData
{
    public float priceRate;//当前价格比例
    public int level;//当前商店等级
    public float Incom;//总收入
    public bool isMoneyEnough;//金币是否充足
    public string groundName;//所在土地的名字
}

/// <summary>
/// 主场景游戏数据
/// </summary>
public class MainSceneData
{
    public string name;//存档名字
    public string sceneName;//游戏场景的名字
    public PlayerData playerData;//玩家信息
    public KnapsackData knapsackData;//背包数据
    public long ticks;//时间戳
    public Dictionary<string,GroundPropertyData> currentGroundDict;//键为土地快的名字，值为土地信息
    //普通建造物信息
    public Dictionary<string,SampleBuildingData> sampleBuildingDict;//键为土地快名字，值为所建造的建造物信息
    //商店建造物信息
    public Dictionary<string,ShopBuildingData> shopBuildingDict;//键为土地名字，值为所建造的建造物信息
    //任务数据
    public Dictionary<string, TaskItemData> taskItemDataDict;//键为任务id，值为任务数据
    //需求数据
    public List<NumDemandData> numDemandList;
}