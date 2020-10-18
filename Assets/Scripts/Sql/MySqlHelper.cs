using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MySqlHelper
{
    private static MySqlHelper _instance;
    private MySqlHelper() { }

    private MySqlAccess _mysql;
    public static MySqlHelper inst
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MySqlHelper();
                _instance.Init();
            }
            return _instance;
        }
    }

    public void Init()
    {
        _mysql = new MySqlAccess();
    }

    public DataSet Execute(string sql)
    {
        Debug.Log(sql);
        return _mysql.Execute(sql);
    }
}
