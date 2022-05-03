using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSizeController : MonoBehaviour
{
    [SerializeField]
    private float circleSize = 1;
    public float circleSizeIncrement; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            circleSize -= circleSizeIncrement;
            UpdateSize();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            circleSize += circleSizeIncrement;
            UpdateSize();
        }
    }
    private void UpdateSize()
    {
        transform.localScale = new Vector3(circleSize, circleSize, 1);
    }
}
