using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timerTxt;
    public UnityAction timeUpAction = delegate { };
    private Coroutine timerCoroutine;

    public void CountDownActive(bool active, int startTime=0)
    {
        if(active)
        {
            timerCoroutine = StartCoroutine(CountDownCoroutine(startTime));
        }
        else
        {
            StopCoroutine(timerCoroutine);
        }
    }
    private IEnumerator CountDownCoroutine(int startTime)
    {
        int time = startTime;
        while(time>=0)
        {
            timerTxt.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        timeUpAction();
    }


}
