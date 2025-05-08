using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 购买之后的建造面板
/// </summary>
public class BuildPanel : BasePanel
{
    public GameObject buildTypeList;
    public GameObject buildList;

    private void Start()
    {
        BuildDataInit();
        EventCenter.Instance.AddEventListener(GameEvent.建造物品成功, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.建造物品成功, Hide);
    }

    /// <summary>
    /// 初始化建造面板的数据
    /// </summary>
    public void BuildDataInit()
    {
        for (int i = 0; i < buildTypeList.transform.childCount; i++)//遍历建造类型的对象
        {
            Button btn = buildTypeList.transform.GetChild(i).GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                OpenCurrentBuildItemList(btn.gameObject.name);//打开对应类型的面板
            });
            string typeName = buildTypeList.transform.GetChild(i).name;//拿到建造物的类型
            //从Resources里克隆出对应面板
            GameObject panel = ResMgr.Instance.load<GameObject>("UI/UIPanel/BuildItemsPanel");
            panel.name = typeName;
            panel.transform.SetParent(buildList.transform);
            panel.transform.localPosition = Vector3.one;
            panel.transform.localScale = Vector3.one;
            panel.transform.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// 显示当前点击的建造面板
    /// </summary>
    /// <param name="typeName">建造类型</param>
    public void OpenCurrentBuildItemList(string typeName)
    {
        for (int i = 0; i < buildList.transform.childCount; i++)
        {
            //if (buildList.transform.GetChild(i).name != typeName)
            //{
            //    buildList.transform.GetChild(i).gameObject.SetActive(false);
            //}
            //else
            //{
            //    buildList.transform.GetChild(i).gameObject.SetActive(true);
            //}等同于下面的写法
            buildList.transform.GetChild(i).gameObject.SetActive(buildList.transform.GetChild(i).name == typeName);
        }
    }
}
