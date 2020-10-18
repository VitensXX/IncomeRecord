using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 交易数据
/// </summary>
public class TradeData
{
    public TradeType tradeType;
    public FundType fundType;
    public DateTime time;
    public int money;//交易额
    public int income;//收益
}