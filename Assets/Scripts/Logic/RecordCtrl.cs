using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordCtrl : MonoBehaviour
{
    public InputField timeInput, moneyInput, incomeInput;
    public Dropdown tradeTypeDropDown;
    public Dropdown fundTypeDropDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickSave()
    {
        try
        {
            TradeData data = new TradeData();
            data.tradeType = (TradeType)tradeTypeDropDown.value;
            data.fundType = (FundType)fundTypeDropDown.value;
            data.time = DateTime.Parse(timeInput.text);
            data.money = Mathf.RoundToInt(float.Parse(moneyInput.text) * 100);
            data.income = Mathf.RoundToInt(float.Parse(incomeInput.text) * 100);

            //TODO 插入数据之前 需要校验是否已经插入过 

            if (DataHelper.InsertOneTradeData(data))
            {
                Debug.LogError("保存成功");
            }
            else
            {
                Debug.LogError("保存失败");
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

  
}
