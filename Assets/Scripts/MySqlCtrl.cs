using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MySqlCtrl : MonoBehaviour
{
    //封装好的数据库类
    MySqlAccess mysql;

    //数据库启动
    public static void Startup()
    {

    }

    private void Start()
    {
        mysql = new MySqlAccess();
    }

    public void OnPointerClick()
    {
        //if (eventData.pointerPress.name == "loginButton")
        //{     //如果当前按下的按钮是注册按钮 
        //    OnClickedLoginButton();
        //}
    }

    /// <summary>
    /// 按下登录按钮
    /// </summary>
    public void OnClickedLoginButton()
    {
        mysql.OpenSql();

        DataSet ds = mysql.Execute("select money from t_trade_data where id = 1");
        //DataSet ds = mysql.Select("t_trade_data", new string[] { "money" }, new string[] { "id" }, new string[] { "=" }, new string[] { "1" });
        if (ds != null)
        {
            DataTable table = ds.Tables[0];
            if (table.Rows.Count > 0)
            {
                //loginMessage.color = Color.green;
                Debug.Log("用户权限等级：" + table.Rows[0][0]);
            }
            else
            {
                //loginMessage.color = Color.red;
            }
            //loginMessage.text = loginMsg;
        }

        mysql.CloseSql();
    }
}
