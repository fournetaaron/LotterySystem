using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOutcome : MonoBehaviour
{
    public List<SymbolDisplay> symbolDisplays;

    public void ResetOutcome()
    {
        foreach (var symbol in symbolDisplays)
        {
            symbol.StopScalingSymbol();
        }
    }

    public void WinPrize()
    {
        foreach(var symbol in symbolDisplays)
        {
            symbol.StartScalingSymbol();
        }
    }

    public void NoPrize()
    {
    }
}
