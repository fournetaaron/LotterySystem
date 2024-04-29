using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolDisplay : MonoBehaviour
{
    public float scaleMaxSize = 2f;

    public float scaleTime = 1f;

    public float scaleIdleTime = 2f;

    public void ResetSymbolScale()
    {
        transform.localScale = Vector3.one;
    }

    public void StartScalingSymbol()
    {
        StopAllCoroutines();

        StartCoroutine(ScaleSymbolUp());
    }

    public void StopScalingSymbol()
    {
        StopAllCoroutines();

        ResetSymbolScale();
    }

    protected IEnumerator ScaleSymbolUp()
    {
        float currentTime = 0;

        while (currentTime < scaleTime)
        {
            currentTime += Time.deltaTime;

            Vector3 adjustedScale = transform.localScale;

            adjustedScale.x = 1f + currentTime / scaleTime * scaleMaxSize;
            adjustedScale.y = 1f + currentTime / scaleTime * scaleMaxSize;

            transform.localScale = adjustedScale;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(scaleIdleTime);

        StartCoroutine(ScaleSymbolDown());
    }

    protected IEnumerator ScaleSymbolDown()
    {
        float currentTime = scaleTime;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            Vector3 adjustedScale = transform.localScale;

            adjustedScale.x = 1f + currentTime / scaleTime * scaleMaxSize;
            adjustedScale.y = 1f + currentTime / scaleTime * scaleMaxSize;

            transform.localScale = adjustedScale;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        ResetSymbolScale();

        yield return new WaitForSeconds(scaleIdleTime);

        StartCoroutine(ScaleSymbolUp());
    }
}
