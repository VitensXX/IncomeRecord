using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public class MySqlAccess
{
    //连接类对象
    public static MySqlConnection mySqlConnection;
    //IP地址 
    private static string host = "localhost";
    //端口号
    private static string port = "3306";
    //用户名
    private static string userName = "root";
    //密码
    private static string password = "123456";
    //数据库名称
    private static string databaseName = "vitensxx_money";

    /// <summary>
    /// 构造方法
    /// </summary>
    public MySqlAccess()
    {
        OpenSql();
    }

    /// <summary>
    /// 打开数据库
    /// </summary>
    public void OpenSql()
    {
        try
        {
            string mySqlString = string.Format("Database={0};DataSource={1};User={2};pwd={3};port={4}"
                , databaseName, host, userName, password, port);
            mySqlConnection = new MySqlConnection(mySqlString);
            mySqlConnection.Open();

        }
        catch (Exception e)
        {
            throw new Exception("服务器连接失败，请重新检查MySql服务是否打开。" + e.Message.ToString());
        }

    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseSql()
    {
        if (mySqlConnection != null)
        {
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            mySqlConnection = null;
        }
    }

    //执行sql语句
    public DataSet Execute(string sql)
    {
        return QuerySet(sql);
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">要查询的列</param>
    /// <param name="whereColumnName">查询的条件列</param>
    /// <param name="operation">条件操作符</param>
    /// <param name="value">条件的值</param>
    /// <returns></returns>
    public DataSet Select(string tableName, string[] items, string[] whereColumnName,
        string[] operation, string[] value)
    {

        if (whereColumnName.Length != operation.Length || operation.Length != value.Length)
        {
            throw new Exception("输入不正确：" + "要查询的条件、条件操作符、条件值 的数量不一致！");
        }
        string query = "Select " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            query += "," + items[i];
        }

        query += " FROM " + tableName + " WHERE " + whereColumnName[0] + " " + operation[0] + " '" + value[0] + "'";
        for (int i = 1; i < whereColumnName.Length; i++)
        {
            query += " and " + whereColumnName[i] + " " + operation[i] + " '" + value[i] + "'";
        }
        return QuerySet(query);

    }

    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sqlString">sql语句</param>
    /// <returns></returns>
    private DataSet QuerySet(string sqlString)
    {
        if (mySqlConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                mySqlAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
            finally
            {
            }
            return ds;
        }
        return null;
    }
}