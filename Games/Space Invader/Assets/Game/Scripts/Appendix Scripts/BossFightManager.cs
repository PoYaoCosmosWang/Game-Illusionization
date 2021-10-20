using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BossFightManager : MonoBehaviour
{
    public Move AroundGroupMove;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCoroutine());
        StartCoroutine(AdjustRotatingSpeed());
        StartCoroutine(FadeInOutCoroutine());
    }

    public float step = 2f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            FadeIn();
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            FadeOut();
        }
    }
    private IEnumerator AdjustRotatingSpeed()
    {

        while(true)
        {
            if (AroundGroupMove.RotatingSpeed >= 360 || AroundGroupMove.RotatingSpeed <= 0)
            {
                step *= -1;
            }
            AroundGroupMove.RotatingSpeed += step;
            yield return new WaitForSeconds(0.005f);
        }

    }



    public Vector3 startPoint;
    public Vector3 endPoint;
    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(2f);
        float interval = 1f;
        float time = 0f;
        while(time<interval)
        {
            transform.position = startPoint + (endPoint - startPoint) * time;
            time += Time.deltaTime;
            yield return null;
        }

    }
    
    

    public SpriteRenderer[] circle;
    private void AdjustAlpha(float value)
    {

        Color insideColor = circle[0].color;
        insideColor.a = value;
        circle[0].color = insideColor;
        Color outsideColor = circle[1].color;
        outsideColor.a = value;
        circle[1].color = outsideColor;

    }
    private IEnumerator LerpColor(float start,float end,int interval)
    {
        float time = 0;
        while(interval>time)
        {
            float i = start + (end - start) * time / interval;
            time += Time.deltaTime;

            AdjustAlpha(i);
            yield return null;
        }
    }
    private void FadeIn()
    {
        StartCoroutine(LerpColor(0, 1, 1));
    }
    private void FadeOut()
    {
        StartCoroutine(LerpColor(1, 0, 1));
    }
    private IEnumerator FadeInOutCoroutine()
    {
        
        yield return new WaitForSeconds(10);
        while (true)
        {
            FadeIn();
            yield return new WaitForSeconds(5);
            FadeOut();
            yield return new WaitForSeconds(10);

        }
    }
}
