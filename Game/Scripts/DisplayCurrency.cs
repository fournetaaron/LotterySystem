using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrency : MonoBehaviour
{
    public TextMeshProUGUI currencyDisplayText;

    public float timeToRollUp = 1f;

    protected string startingText;

    protected int currentAmountDisplay = 0;

    private void Awake()
    {
        startingText = currencyDisplayText.text;
    }

    public void UpdateCurrency(int amount = 0)
    {
        StopAllCoroutines();

        if (amount - currentAmountDisplay > 0)
        {
            StartCoroutine(CountUpAmount(amount));
            return;
        }

        currencyDisplayText.text = startingText + amount.ToString();
    }

    protected IEnumerator CountUpAmount(int amount = 0)
    {
        float currentTime = 0;

        int startingAmount = currentAmountDisplay;

        amount -= currentAmountDisplay;

        while (currentTime < timeToRollUp)
        {
            currentTime += Time.deltaTime;

            currentAmountDisplay = startingAmount + (int)(currentTime / timeToRollUp * amount);

            currencyDisplayText.text = startingText + currentAmountDisplay.ToString();

            yield return new WaitForSeconds(Time.deltaTime);
        }

        currentAmountDisplay = startingAmount + amount;

        currencyDisplayText.text = startingText + currentAmountDisplay;
    }
}