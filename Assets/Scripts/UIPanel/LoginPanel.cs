using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 登录面板  每次打开登录面板  重新连接 数据库  关掉面板关闭数据库连接   
/// </summary>
public class LoginPanel : BasePanel
{
    public Text StateText;
    public Dropdown dropdown;
    private List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
    private ServerData currentServerData;
    private List<ServerData> serverDataList = new List<ServerData>();
    private Dictionary<string, ServerData> serverDataDict = new Dictionary<string, ServerData>();
    public Button loginBtn;
    public Button registerBtn;
    public Text username;
    public Text password;

    /// <summary>
    /// 激活面板
    /// </summary>
    private void OnEnable()
    {
        if (!DBManager.Instance.OpenConnection()) //链接失败
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("数据库连接失败");
            return;
        }

        //更新选服信息  
        UpdateServerData();
        //给下拉列表添加值更改的监听方法 
        dropdown.onValueChanged.AddListener((value) =>
        {
            currentServerData = serverDataDict[optionDatas[value].text];
            StateText.text = currentServerData.State;
            StateText.color = currentServerData.State == "爆满" ? Color.red : Color.green;
        });
    }

    /// <summary>
    /// 更新选服信息的方法  
    /// </summary>
    private void UpdateServerData() //到这里说明数据库连接成功 
    {
        serverDataDict = new Dictionary<string, ServerData>();
        optionDatas = new List<Dropdown.OptionData>();  
        //需要有一个Options列表  
        optionDatas = new List<Dropdown.OptionData>();
        serverDataDict = new Dictionary<string, ServerData>();
        //需要一条查询语句  
        string query = "SELECT * FROM server;";
        DataTable dt = new DataTable();
        dt = DBManager.Instance.SelectQuery(query); //查询对应的选服信息  
        foreach (DataRow data in dt.Rows) //每一行的信息  
        {
            //拿到每一行的信息生成一个serverData的数据对象   还要把这个存到我们的字典中 
            ServerData serverdata = new ServerData
            {
                ServerId = int.Parse(data[0].ToString()),
                ServerIp = data[1].ToString(),
                State = data[2].ToString(),
                ServerName = data[3].ToString()
            };
            //添加到optionDatas列表中  
            Dropdown.OptionData optionData = new Dropdown.OptionData(serverdata.ServerName);
            optionDatas.Add(optionData);
            serverDataDict.Add(serverdata.ServerName, serverdata);
        }

        //添加到我们的dropdown
        dropdown.options=optionDatas;
        //给我们当前选中的数据对象赋值  
        currentServerData = serverDataDict[optionDatas[0].text];
    }

    /// <summary>
    /// 失活面板 
    /// </summary>
    private void OnDisable()
    {
    }

    void Start()
    {
        loginBtn.onClick.AddListener(loginCheck);
        registerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.openPanel<RegisterPanel>();
            UIManager.Instance.closePanel<LoginPanel>();
        });
    }

    /// <summary>
    /// 登录核对的方法 
    /// </summary>
    private void loginCheck()
    {
        //先核对服务器信息  
        switch (currentServerData.State)
        {
            case "爆满":
                UIManager.Instance.openPanel<TipPanel>().UpdateTipText("当前服务器爆满请选择其他服务器");
                break;
            default:
                //判断用户名是否存在  
                //先有一个查询语句  查询用户名 
                string query = $"SELECT * FROM user WHERE name = '{username.text.Trim()}' LIMIT 1";
                DataTable dt = new DataTable();
                dt = DBManager.Instance.SelectQuery(query);
                if (dt.Rows.Count > 0) //说明查到了  
                {
                    string pwd = dt.Rows[0]["password"].ToString();
                    //判断密码是否一致  
                    if (pwd == password.text.Trim())
                    {
                        UIManager.Instance.openPanel<TipPanel>().UpdateTipText("登录成功");
                    }
                    else
                    {
                        UIManager.Instance.openPanel<TipPanel>().UpdateTipText("密码错误");
                        
                    }
                }
                else
                {
                    UIManager.Instance.openPanel<TipPanel>().UpdateTipText("用户名不存在");
                }

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}