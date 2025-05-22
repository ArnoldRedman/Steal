using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerData CurrPlayerData;
    //存储建造物的字典
    public Dictionary<string,BuildItemData> buildItemDict = new Dictionary<string, BuildItemData>();
    public TimeController timerController;

    //产出物品信息字典
    public Dictionary<string,ProductItemData> productItemDict;
    //背包数据对象
    public KnapsackData knapsack;
    //消耗物品的列表
    public List<XiaohaoItemData> xiaohaoList = new List<XiaohaoItemData>();
    //售卖商品列表
    public List<MerchantingData> merchantingList = new List<MerchantingData>();
    //对话数据的字典
    public Dictionary<string, DialogueItemData> dialogueItemDict = new Dictionary<string, DialogueItemData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //UIManager.Instance.openPanel<StartPanel>();

        //buildItemDict = JsonMgr.Instance.LoadData<List<BuildItemData>>("tbbuilditem");
        Init();
    }

    private void Init()
    {
        //初始化UI
        UIManager.Instance.openPanel<PlayerPropPanel>();
        //初始化游戏数据
        GameDateInit();
        //初始化时间模块
        GameTimeInit();
    }

    private void GameTimeInit()
    {
        timerController = new TimeController();
        MonoMgr.Instance.addUpdateListener(timerController.UpdateTime);
    }

    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    private void GameDateInit()
    {
        //初始化玩家信息
        PlayerDataInit();
        //初始化物品建造信息
        BuildItemDataInit();
        //初始化产出物品信息
        ProductItemDataInit();
        //初始化消耗物品的信息
        xiaohaoItemDataInit();
        //初始化背包信息
        KnapsackDataInit();
        //初始化商店商品的字典数据
        MerchantingDataInit();
        //初始化对话信息的字典
        DialogueItemDataInit();
    }

    /// <summary>
    /// 对话数据初始化
    /// </summary>
    private void DialogueItemDataInit()
    {
        //对话数据列表
        List<DialogueItemData> dialogueItemDataList =
            JsonMgr.Instance.LoadData<List<DialogueItemData>>("tbdialogueitem");
        //保存到字典中
        foreach (var data in dialogueItemDataList)
        {
            dialogueItemDict.Add(data.id, data);
        }
    }

    /// <summary>
    /// 初始化商店商品的字典
    /// </summary>
    private void MerchantingDataInit()
    {
        merchantingList = JsonMgr.Instance.LoadData<List<MerchantingData>>("tbmerchantingitem");
    }

    /// <summary>
    /// 初始化消耗物品信息
    /// 保存消耗的列表
    /// </summary>
    private void xiaohaoItemDataInit()
    {
        xiaohaoList = JsonMgr.Instance.LoadData<List<XiaohaoItemData>>("tbxiaohaoitem");
    }

    /// <summary>
    /// 提供方法获取消耗物品的字典
    /// </summary>
    /// 建造id
    /// 产出物品id
    /// 返回消耗物品的字典
    public Dictionary<string,int> GetXiaohaoItemDict(string buildId,string productId)
    {
        //没有消耗物品 //有消耗物品
        foreach (var item in xiaohaoList)
        {
            //找到了对应消耗物品的信息
            if (item.buildId == buildId && item.productId == productId)
            {
                return item.XiaohaoDict;
            }
        }

        return null;
    }

    /// <summary>
    /// 初始化背包数据
    /// </summary>
    private void KnapsackDataInit(KnapsackData newKnapsackData=null)
    {
        knapsack = new KnapsackData { productDict = new Dictionary<string, int>() };
        if (newKnapsackData != null)//加载存档的背包信息
        {
            knapsack=newKnapsackData;
        }
        else//新开存档的游戏 初始化背包数据
        {
            foreach (var id in productItemDict.Keys)
            {
                knapsack.productDict.Add(id,0);//所有物品默认都是0
            }
        }
    }

    /// <summary>
    /// 初始化产出物品信息
    /// </summary>
    private void ProductItemDataInit()
    {
        productItemDict = new Dictionary<string, ProductItemData>();
        List<ProductItemData> productItemDataList = new List<ProductItemData>();
        productItemDataList = JsonMgr.Instance.LoadData<List<ProductItemData>>("tbproductitem");
        foreach (var itemData in productItemDataList)
        {
            productItemDict.Add(itemData.id,itemData);//id 和 物品信息
        }
    }

    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    private void PlayerDataInit()
    {
        CurrPlayerData = new PlayerData { Coin = 200000, name = "Jack",GameLevel = 0};
    }

    private void BuildItemDataInit()
    {
        buildItemDict = new Dictionary<string, BuildItemData>();
        List<BuildItemData> buildItemDataList = new List<BuildItemData>();
        buildItemDataList = JsonMgr.Instance.LoadData<List<BuildItemData>>("tbbuilditem");
        foreach (BuildItemData buildItemData in buildItemDataList)
        {
            //往字典中添加新项
            buildItemDict.Add(buildItemData.id,buildItemData);
        }
    }

}
