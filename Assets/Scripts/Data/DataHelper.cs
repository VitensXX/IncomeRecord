using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// 数据操作
/// </summary>
public class DataHelper
{
    public static bool InsertOneTradeData(TradeData data)
    {
        string sql = string.Format( "INSERT INTO t_trade_data (type,money,time,fund_type,income) " +
            "VALUES ({0},{1},'{2}',{3},{4});", (int)data.tradeType, data.money, data.time, (int)data.fundType, data.income);
        return MySqlHelper.inst.Execute(sql) != null;
    }

    //获取总共数据记录数
    public static int GetTradeDataCount()
    {
        string sql = "SELECT count(*) FROM t_trade_data";

        DataSet ds = MySqlHelper.inst.Execute(sql);
        DataTable table = ds.Tables[0];
        return int.Parse(table.Rows[0][0].ToString());
    }

    //求总收益
    public static string GetTotalIncome()
    {
        string sql = "SELECT SUM(income) FROM t_trade_data";
        DataSet ds = MySqlHelper.inst.Execute(sql);
        DataTable table = ds.Tables[0];

        int sum = int.Parse(table.Rows[0][0].ToString());

        return (sum / 100.0f).ToString("0.00");
    }

    //收益详情
    public static Dictionary<FundType, string> GetIncomeDetail()
    {
        string sql = "SELECT fund_type,income FROM t_trade_data";
        DataSet ds = MySqlHelper.inst.Execute(sql);
        DataTable table = ds.Tables[0];

        Dictionary<FundType, string> detail = new Dictionary<FundType, string>();

        Dictionary<FundType, int> detailInt = new Dictionary<FundType, int>();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            DataRow dataRow = table.Rows[i];
            FundType fundType = (FundType)int.Parse(dataRow["fund_type"].ToString());

            if (!detailInt.ContainsKey(fundType))
            {
                detailInt[fundType] = 0;
            }

            detailInt[fundType] += int.Parse(dataRow["income"].ToString());
        }

        foreach (var item in detailInt)
        {
            detail[item.Key] = (item.Value / 100.0f).ToString("0.00");
        }

        return detail;
    }

    //正在使用中
    public static string GetTotalInUse()
    {
        string sql = "SELECT type,money,income FROM t_trade_data";
        DataSet ds = MySqlHelper.inst.Execute(sql);
        DataTable table = ds.Tables[0];

        int inUse = 0;
        for (int i = 0; i < table.Rows.Count; i++)
        {
            DataRow dataRow = table.Rows[i];
            TradeType tradType = (TradeType)int.Parse(dataRow["type"].ToString());
            if (tradType == TradeType.Buy)
            {
                inUse += int.Parse(dataRow["money"].ToString());
            }
            else
            {
                int saleMoney = int.Parse(dataRow["money"].ToString()) - int.Parse(dataRow["income"].ToString());
                inUse -= saleMoney;
            }
        }

        return (inUse / 100.0f).ToString("0.00");
    }

    //获取使用详情
    public static Dictionary<FundType, string> GetUsingDetail()
    {
        string sql = "SELECT type,fund_type,money,income FROM t_trade_data";
        DataSet ds = MySqlHelper.inst.Execute(sql);
        DataTable table = ds.Tables[0];

        Dictionary<FundType, string> detail = new Dictionary<FundType, string>();

        Dictionary<FundType, int> detailInt = new Dictionary<FundType, int>();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            DataRow dataRow = table.Rows[i];
            TradeType tradType = (TradeType)int.Parse(dataRow["type"].ToString());
            FundType fundType = (FundType)int.Parse(dataRow["fund_type"].ToString());

            if (!detailInt.ContainsKey(fundType))
            {
                detailInt[fundType] = 0;
            }

            if (tradType == TradeType.Buy)
            {
                detailInt[fundType] += int.Parse(dataRow["money"].ToString());
            }
            else
            {
                int saleMoney = int.Parse(dataRow["money"].ToString()) - int.Parse(dataRow["income"].ToString());
                detailInt[fundType] -= saleMoney;
            }
        }

        foreach (var item in detailInt)
        {
            detail[item.Key] = (item.Value / 100.0f).ToString("0.00");
        }

        return detail;
    }
}
