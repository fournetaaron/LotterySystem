using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealCell : MonoBehaviour
{
    //public float dissolveTime = 1f;

    public MeshRenderer meshRenderer;

    private void Start()
    {
        ResetAlpha();
    }

    public void Hide(float revealTime)
    {
        StartCoroutine(HideOverTime(revealTime));

        //StartCoroutine(HideOverTime());
    }

    public void Reveal(float revealTime)
    {
        StartCoroutine(RevealOverTime(revealTime));

        //StartCoroutine(RevealOverTime());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void ResetAlpha()
    {
        SetAlpha(0f);
    }

    public void SetAlpha(float alpha)
    {
        meshRenderer.material.SetFloat("_Alpha", alpha);
    }

    protected IEnumerator HideOverTime(float dissolveTime)
    {
        float currentTime = dissolveTime;

        while (currentTime >= 0f)
        {
            currentTime -= Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, currentTime);

            SetAlpha(alpha);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        SetAlpha(0f);
    }

    protected IEnumerator RevealOverTime(float revealTime)
    {
        float currentTime = 0;

        while (currentTime < revealTime)
        {
            currentTime += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, currentTime);

            SetAlpha(alpha);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        SetAlpha(1f);
    }
}
