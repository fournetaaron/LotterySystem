using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class DisplayCellData
{
    public TextMeshPro cellText;
    public RevealCell cellReveal;
}

public class DisplayCellsController : MonoBehaviour
{
    public GameObject startGameButton;

    public GameObject slamRevealButton;

    public TextMeshProUGUI playText;

    public List<DisplayCellData> cellButtonsList;

    public float revealTime = 0;

    protected bool inPlay = false;

    [HideInInspector]
    public UnityEvent revealDisplayDone;

    private void Start()
    {
        playText.text = "Press enter to play";

        startGameButton.SetActive(true);
        slamRevealButton.SetActive(false);

        inPlay = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inPlay)
            {
                SlamRevealCells();
            }
        }
    }

    public void UpdateCellSymbols(string evaluation)
    {
        for (int i = 0; i < evaluation.Length; ++i)
        {
            cellButtonsList[i].cellText.text = evaluation[i].ToString();
        }

        RevealAllCells();
    }

    public void RevealAllCells()
    {
        if (inPlay)
        {
            SlamRevealCells();
            return;
        }

        playText.text = "Press space to slam";

        //startGameButton.SetActive(false);
        //slamRevealButton.SetActive(true);

        inPlay = true;

        StartCoroutine(RevealCellsOverTime());
    }

    public void SlamRevealCells()
    {
        StopAllCoroutines();

        playText.text = "Press enter to play";

        startGameButton.SetActive(true);
        slamRevealButton.SetActive(false);

        inPlay = false;

        foreach (var cell in cellButtonsList)
        {
            cell.cellReveal.Stop();
            cell.cellReveal.SetAlpha(1f);
        }

        revealDisplayDone?.Invoke();
    }

    public void ResetAllCells()
    {
        foreach (var cell in cellButtonsList)
        {
            cell.cellReveal.Stop();
            cell.cellReveal.ResetAlpha();
        }
    }

    //public void RevealCell(int index)
    //{
    //    cellButtonsList[index].cellReveal.Reveal();
    //}

    protected IEnumerator RevealCellsOverTime()
    {
        int index = 0;

        float revealTimePerCell = revealTime / cellButtonsList.Count;

        while (index < cellButtonsList.Count)
        {
            cellButtonsList[index].cellReveal.Reveal(revealTimePerCell);

            yield return new WaitForSeconds(revealTimePerCell);

            ++index;
        }

        playText.text = "Press enter to play";

        startGameButton.SetActive(true);
        slamRevealButton.SetActive(false);

        inPlay = false;

        revealDisplayDone?.Invoke();
    }
}
