using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LuckySpinData
{
    public string pattern;
    public int weight;
}

public class LuckySpinEvaluation : MonoBehaviour
{
    public List<LuckySpinData> luckySpinDataList;

    protected int totalLuckySpinWeight = 0;

    protected string outcome;

    private void Start()
    {
        foreach (var luckSpinData in luckySpinDataList)
        {
            totalLuckySpinWeight += luckSpinData.weight;
        }
    }

    public string CheckForLuckySpin()
    {
        int randomWeight = UnityEngine.Random.Range(0, totalLuckySpinWeight);

        int currentWeight = 0;

        int index = 0;

        while (index < luckySpinDataList.Count)
        {
            currentWeight += luckySpinDataList[index].weight;

            if (currentWeight > randomWeight)
                return luckySpinDataList[index].pattern;

            ++index;

        }

        return string.Empty;
    }
}
