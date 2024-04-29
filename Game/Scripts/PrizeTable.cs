using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrizeData
{
    public string prizePattern;
    public string prizeName;
    public int amount;
}

public class PrizeTable : MonoBehaviour
{
    public List<PrizeData> prizeDataList;

    public PrizeData CheckPrizeData(string evalPattern)
    {
        UnityEngine.Debug.Log("Pattern: " + evalPattern);

        foreach (var prizeData in prizeDataList)
        {
            if (prizeData.prizePattern.Contains(evalPattern))
            {
                UnityEngine.Debug.Log("Prize won: " + prizeData.prizeName);

                return prizeData;
            }
        }

        return null;
    }
}
