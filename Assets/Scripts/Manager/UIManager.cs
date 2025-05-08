using System.Collections.Generic;
//using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UIManager : UnitySingleTonMono<UIManager>
{

    //控制面板开关   获取面板的脚本  在打开面板的时候把面板存到字典中      
    //字典 来存储当前所有打开的面板    创建一个面板的基类 所有的面板都继承这个基类 
    public Dictionary<string, BasePanel> UIPanelDict = new Dictionary<string, BasePanel>();
    //保存 Canvas  
    [HideInInspector]
    public RectTransform Canvas;
    //当前所有要显示的面板的父亲对象
    private GameObject CurrentShowPanel;


    /// <summary>
    /// T 代表我们要创建的面板的名字  面板的名字一定要和脚本的名字一样  
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public T openPanel<T>() where T : BasePanel
    {
        //打开面板之前要保证一定要有 CurrentShowPanel
        if (CurrentShowPanel == null)
        {
            CurrentShowPanel = new GameObject();
            CurrentShowPanel.name = "CurrentShowPanel";
            CurrentShowPanel.transform.SetParent(Canvas.transform);
            CurrentShowPanel.transform.localPosition = Vector3.zero;
            CurrentShowPanel.transform.localScale = Vector3.one;
        }
        //要创建的面板的名字  
        string panelName = typeof(T).Name;
        //寻找面板 首先从字典中寻找  
        if (UIPanelDict.ContainsKey(panelName))
        {
            UIPanelDict[panelName].Show();//打开面板的方法 
            UIPanelDict[panelName].transform.SetParent(CurrentShowPanel.transform);
            return UIPanelDict[panelName] as T;
        }
        else//不在字典中 创建一个新的面板 存到我们的字典中 
        {
            GameObject panelObj = Resources.Load<GameObject>("UI/UIPanel/" + panelName);
            //克隆面板到我们的Canvas容器下  我们现在还没有容器  容器应该在哪里手动生成呢  Awake 
            panelObj = GameObject.Instantiate<GameObject>(panelObj);
            panelObj.transform.SetParent(CurrentShowPanel.transform);
            BasePanel panel = panelObj.GetComponent<T>() as BasePanel;
            panelObj.transform.localPosition = Vector3.zero;
            panelObj.transform.localScale = Vector3.one;
            panelObj.name = typeof(T).Name;
            //保存到字典中  
            UIPanelDict.Add(panelName, panel);
            return panel as T;
        }







    }
    //直接这么写有没有问题
    public override void Awake()
    {
        base.Awake();   //执行父类中的Awake方法
        //克隆一个 canvas 存在整个游戏中 EventSystem  
        GameObject canvasObj = Resources.Load<GameObject>("Canvas");
        canvasObj = GameObject.Instantiate(canvasObj);
        canvasObj.name = "Canvas";
        Canvas = canvasObj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(canvasObj);
        GameObject eventSystemObj = Resources.Load<GameObject>("EventSystem");
        eventSystemObj = GameObject.Instantiate(eventSystemObj);
        eventSystemObj.name = "EventSystem";
        GameObject.DontDestroyOnLoad(eventSystemObj);

    }
    /// <summary>
    /// 关闭面板 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void closePanel<T>()
    {
        if (UIPanelDict.ContainsKey(typeof(T).Name))
        {
            UIPanelDict[typeof(T).Name].Hide();  //单纯把面板隐藏 不销毁
            // GameObject.Destroy(UIPanelDict[typeof(T).Name].gameObject);
            // UIPanelDict.Remove(typeof(T).Name); 
        }

    }

    public T getPanel<T>() where T : BasePanel
    {
        string name = typeof(T).Name;
        if (UIPanelDict.ContainsKey(name))
            return UIPanelDict[name] as T;
        return null;

    }

}
