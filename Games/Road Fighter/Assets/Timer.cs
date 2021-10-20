using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text UItxt;
    private int time=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartTimer()
    {
        StartCoroutine(TimerCoroutine());
    }
    private IEnumerator TimerCoroutine()
    {
        
        while(true)
        {
            UItxt.text = "Survive Time: " + time.ToString();
            print(time);
            yield return new WaitForSeconds(1);
            time++;
        }
    }
}
