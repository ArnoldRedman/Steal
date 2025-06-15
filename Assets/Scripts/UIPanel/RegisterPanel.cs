using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Text username;
    public Text password;
    public Text pwsCheck;
    public Button registerBtn;
    public Button loginBtn;

    void Start()
    {
        registerBtn.onClick.AddListener(registerCheck);
        loginBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.openPanel<LoginPanel>();
            UIManager.Instance.closePanel<RegisterPanel>();
        });
    }

    /// <summary>
    /// 核对注册方法
    /// </summary>
    private void registerCheck()
    {
        //首先先判断用户存不在   如果存在就要换一个名字  查询语句 
        string query = $"SELECT COUNT(*) FROM user WHERE name='{username.text}'";
        //插入新的用户   插入语句 
        string inserQuery =
            $"INSERT INTO user (name,password) VALUES('{username.text.Trim()}','{password.text.Trim()}')";
        if (password.text.Trim() != pwsCheck.text.Trim()||password.text.Trim()=="")
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("密码输入有问题");
            return;
        }

        ;
        if (DBManager.Instance.OpenConnection())
        {
            DataTable dt = DBManager.Instance.SelectQuery(query);
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                UIManager.Instance.openPanel<TipPanel>().UpdateTipText("用户名已存在");
            }
            else
            {
                try
                {
                    DBManager.Instance.NonQuery(inserQuery);
                    UIManager.Instance.openPanel<TipPanel>().UpdateTipText("注册成功");
                    
                }
                catch (MySqlException ex)
                {
                    print(ex.Message);
                    UIManager.Instance.openPanel<TipPanel>().UpdateTipText("注册失败");
                }
            }
        }
        else
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("数据库连接失败");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}