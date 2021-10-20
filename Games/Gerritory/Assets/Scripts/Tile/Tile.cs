using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    private Color _curColor;
    protected virtual Color CurColor
    {
        get
        {
            return _curColor;
        }
        set
        {
            _curColor = value;
            if (FadeCoroutine != null)
            {
                //還在變色中，不要改color
                return;
            }
            meshRenderer.material.SetColor("_Color", _curColor);
        }
    }
    protected Coroutine FadeCoroutine;
    // Start is called before the first frame update
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        CurColor = meshRenderer.material.color;
    }

    public void FadeOutThenFadeIn(float timeInterval)
    {
        FadeCoroutine = StartCoroutine(FadeOutThenFadeInCoroutine(timeInterval));
    }
    private IEnumerator FadeOutThenFadeInCoroutine(float timeInterval)
    {
        Coroutine fadeout = StartCoroutine(FadeOutCoroutine());
        yield return fadeout;
        yield return new WaitForSeconds(timeInterval);
        Coroutine fadein = StartCoroutine(FadeInCoroutine());
        yield return fadein;
        FadeCoroutine = null;
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }
    private IEnumerator FadeInCoroutine()
    {
        float t = 0f;
        while (t <= 1f)
        {
            Color prevColor, afterColor;
            prevColor = CurColor;
            prevColor.a = 0f;
            afterColor = CurColor;
            Color c = Color.Lerp(prevColor, afterColor, t);
            t += Time.deltaTime;
            meshRenderer.material.color = c;
            yield return null;
        }
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    private IEnumerator FadeOutCoroutine()
    {
        float t = 0f;
        while (t <= 1f)
        {
            
            Color prevColor, afterColor;
            prevColor = CurColor;
            afterColor = CurColor;
            afterColor.a = 0f;
            Color c = Color.Lerp(prevColor, afterColor, t);
            t += Time.deltaTime;
            meshRenderer.material.color = c;
            yield return null;
        }

    }



}
