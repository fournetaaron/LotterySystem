using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public DisplayCurrency displayCurrency;

    public int bank;

    private void Start()
    {
        displayCurrency.UpdateCurrency(bank);
    }

    public void AddToBank(int amount)
    {
        bank += amount;
        displayCurrency.UpdateCurrency(bank);
    }

    public bool SubFromBank(int amount)
    {
        if (bank < amount)
            return false;

        bank -= amount;
        displayCurrency.UpdateCurrency(bank);

        return true;
    }
}
