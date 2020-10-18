using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //链接数据库

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //销毁处理
    private void OnDestroy()
    {
        //断开数据库

    }

    public void Test()
    {
        TradeData data = new TradeData();

        //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        data.time = new DateTime(2020, 10, 10);
        data.money = 311;
        data.income = 10;
        data.fundType = FundType.FiveG;
        data.tradeType = TradeType.Sale;
        DataHelper.InsertOneTradeData(data);
    }
}
