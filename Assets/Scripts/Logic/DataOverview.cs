using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数据总览
/// </summary>
public class DataOverview : MonoBehaviour
{
    private Text _totalCountText;
    private Text _totalIncome;
    private Text _inUse;
    private Text _inUseDetail;
    private Text _incomeDetail;

    void Start()
    {
        _totalCountText = transform.Find("Content/totalCount").GetComponent<Text>();
        _totalIncome = transform.Find("Content/totalIncome").GetComponent<Text>();
        _incomeDetail = transform.Find("Content/incomeDetail").GetComponent<Text>();
        _inUse = transform.Find("Content/inUse").GetComponent<Text>();
        _inUseDetail = transform.Find("Content/inUseDetail").GetComponent<Text>();

        Refresh();
    }

    public void Refresh()
    {
        _totalCountText.text = "交易次数：" + DataHelper.GetTradeDataCount();
        _totalIncome.text = "总收益：" + DataHelper.GetTotalIncome();
        _inUse.text = "使用中：" + DataHelper.GetTotalInUse();

        Dictionary<FundType, string> inUseDetail = DataHelper.GetUsingDetail();
        _inUseDetail.text = "使用详情：";
        foreach (var item in inUseDetail)
        {
            _inUseDetail.text += "\n"+item.Key.ToString()+" "+item.Value;
        }

        //收益详情
        Dictionary<FundType, string> incomeDetail = DataHelper.GetIncomeDetail();
        _incomeDetail.text = "收益详情：";
        foreach (var item in incomeDetail)
        {
            _incomeDetail.text += "\n" + item.Key.ToString() + " " + item.Value;
        }
    }
}
