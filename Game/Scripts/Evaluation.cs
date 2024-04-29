using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SymbolData
{
    public string symbol;
    public int weight;
    public bool isWild = false;
}

public class Evaluation : MonoBehaviour
{
    public LuckySpinEvaluation luckySpinEvaluation;

    public DisplayCellsController displayCellsController;

    public Currency currency;

    public PrizeTable prizeTable;

    public DisplayOutcome displayOutcome;

    public int cells = 3;

    public List<SymbolData> symbolsList;

    public int luckySpinChance = 50;

    public string startingSymbols;

    protected int gamesPlayed = 0;

    protected int totalSymbolWeight = 0;

    protected string outcome;

    protected bool inPlay = false;

    protected List<string> history;

    private void Awake()
    {
        history = new List<string>();
    }

    private void Start()
    {
        displayCellsController.revealDisplayDone.AddListener(CheckForWin);

        foreach (var symbol in symbolsList)
        {
            totalSymbolWeight += symbol.weight;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (inPlay)
        {
            displayCellsController.SlamRevealCells();
            return;
        }

        if (currency.SubFromBank(1))
        {
            GenerateOutcome(startingSymbols);
        }
    }

    protected void GenerateOutcome(string evalPattern = "")
    {
        displayOutcome.ResetOutcome();

        ++gamesPlayed;

        displayCellsController.ResetAllCells();

        outcome = evalPattern;

        inPlay = true;

        if (UnityEngine.Random.Range(0, 100) < luckySpinChance)
        {
            UnityEngine.Debug.Log("Luck spin active!");
            outcome = luckySpinEvaluation.CheckForLuckySpin();
        }

        for (int i = outcome.Length; i < cells; ++i)
        {
            outcome += GenerateRandomWeightedSymbol();
        }

        displayCellsController.UpdateCellSymbols(outcome);

        history.Add(outcome);
    }

    protected void CheckForWin()
    {
        inPlay = false;

        PrizeData prize = prizeTable.CheckPrizeData(outcome);

        foreach (var symbol in symbolsList)
        {
            if (symbol.isWild)
            {
                if (outcome.Contains(symbol.symbol))
                {
                    string adjustedOutcome = outcome.Replace(symbol.symbol, "");

                    prize = prizeTable.CheckPrizeData(adjustedOutcome);
                }
            }
        }

        if (prize == null)
        {
            UnityEngine.Debug.Log("Loss");
            displayOutcome.NoPrize();
            return;
        }

        displayOutcome.WinPrize();

        if (CheckForBonus(prize))
        {
            UnityEngine.Debug.Log("Bonus code here");
        }

        currency.AddToBank(prize.amount);
    }

    protected bool CheckForBonus(PrizeData prize)
    {
        if (prize.prizeName.Contains("Bonus"))
            return true;

        return false;
    }

    protected string GenerateRandomWeightedSymbol()
    {
        int randomWeight = UnityEngine.Random.Range(0, totalSymbolWeight);

        int currentWeight = 0;

        foreach (var symbol in symbolsList)
        {
            currentWeight += symbol.weight;

            if (currentWeight > randomWeight)
            {
                return symbol.symbol;
            }
        }

        return string.Empty;
    }
}
