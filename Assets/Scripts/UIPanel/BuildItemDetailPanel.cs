using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildItemDetailPanel : BasePanel
{
    public Text title;
    public Text productName;
    public Image productIcon;
    public Button changeProductBtn;//切换产出物品的按钮
    public Text productDescription;//物品产出描述
    public Text productTimeDescription;//收获的时间描述
    public Text chengben;//成本
    public Button removeBtn;//拆除按钮
    public GameObject xiaohao;//消耗
    public BuildItemBase currBuildItem;//当前点到的建造物
    public string currBuildId;
    public string currProductId;
    private bool isOpen = false;//当前详情是否打开

    private void OnEnable()
    {
        StartCoroutine(DelayedLayoutUpdate());
        Init();//初始化详情面板
        UpdateEveryData();
    }

    private void OnDisable()
    {
        StopCoroutine(DelayedLayoutUpdate());
        isOpen = false;
    }

    /// <summary>
    /// 更新每天要刷新的信息
    /// </summary>
    private void UpdateEveryData()
    {
        if (!isOpen)
        {
            return;
        }
        productTimeDescription.text = $"距收获还有{currBuildItem.shouhuoTime}天";
        UpdateState();
    }

    /// <summary>
    /// 更新每日状态信息
    /// </summary>
    private void UpdateState()
    {
        
    }

    /// <summary>
    /// 每次打开详情面板时初始化
    /// </summary>
    private void Init()
    {
        isOpen = true;//说明当前面板处于打开状态
        //当前建造物
        currBuildItem = BuildController.Instance.currGround.transform.Find("Building").GetComponent<BuildItemBase>();
        //拿到当前的建造信息
        BuildItemData buildItemDate = GameManager.instance.buildItemDict[currBuildItem.buildid];
        //建造物的标题
        title.text = buildItemDate.name;
        currBuildId = currBuildItem.buildid;
        currProductId = currBuildItem.productItemId;
        //产品信息更新
        UpdateData();//更换按钮之后要更新的相关内容
        //产能信息更新

        //耗材更新

        //状态更新
    }

    /// <summary>
    /// 更新消耗物品
    /// </summary>
    private void UpdateXiaohaoItem(string buildId,string productId)
    {
        //if (currProductId == productId && currBuildId == buildId)
        //{
        //    return;//点的两个是同一类型的建筑物，不需要更新
        //}

        //先清除原来的物品信息
        for (int i = 0; i < xiaohao.transform.childCount; i++)
        {
            Destroy(xiaohao.transform.GetChild(i).gameObject);
        }
        //显示消耗物品
        //拿到消耗物品的字典
        Dictionary<string, int> xiaohaoDict = GameManager.instance.GetXiaohaoItemDict(buildId, productId);
        if (xiaohaoDict!=null)
        {
            foreach (var id in xiaohaoDict.Keys)
            {
                //克隆消耗物品到xiaohao下面
                GameObject xiaohaoItem = ResMgr.Instance.load<GameObject>("UI/XiaoHaoItem",xiaohao.transform);
                xiaohaoItem.GetComponent<XiaoHaoItem>().UpdateData(GameManager.instance.productItemDict[id], xiaohaoDict[id]);
            }
        }
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener(GameEvent.日期时间每日更新事件,UpdateEveryData);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.日期时间每日更新事件, UpdateEveryData);
    }

    /// <summary>
    /// 更换按钮之后要更新的相关内容
    /// </summary>
    private void UpdateData()
    {
        //拿到当前的建造信息
        BuildItemData buildItemDate = GameManager.instance.buildItemDict[currBuildItem.buildid];
        //产出物品的字典
        Dictionary<string, ProductItemData> productDict = GameManager.instance.productItemDict;
        //拿到产出物品
        ProductItemData currProduct = productDict[currBuildItem.productItemId];
        //产品信息更新
        UpdateProductData(currProduct);
        //产能信息更新
        UpdateProductEfficient(buildItemDate, currProduct);
        //每日要更新的信息
        UpdateEveryData();
        //耗材信息更新 建造id 产出物品id
        UpdateXiaohaoItem(currBuildItem.buildid,currProduct.id);
    }

    /// <summary>
    /// 更新产品信息
    /// </summary>
    /// <param name="currProduct"></param>
    private void UpdateProductData(ProductItemData currProduct)
    {
        //当前产出物品的名字
        productName.text = currProduct.name;
        //当前产出物品的图片
        productIcon.sprite = ResMgr.Instance.load<Sprite>("Sprite/" + currProduct.sprite);
    }

    /// <summary>
    /// 更新产能信息
    /// </summary>
    /// <param name="buildItemDate"></param>
    /// <param name="currProduct"></param>
    private void UpdateProductEfficient(BuildItemData buildItemDate, ProductItemData currProduct)
    {
        //每多少天，产出多少单位的什么东西
        productDescription.text = $"每{buildItemDate.ripeningTime}天，产出{buildItemDate.product[currProduct.id]}{currProduct.unit}{currProduct.name}";  
    }
}
