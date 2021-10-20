using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 Center;
    [Range(-1, 1)]
    public int ClockWise;
    [Range(0, 360f)]
    public float RotatingSpeed;
    public float radius;
    public float angle;
    public ParticleSystem Trail;
    
    void Start()
    {
        HidePath();
    }

    public void RevealPath()
    {
        var emission = Trail.emission;
        emission.enabled = true;
    
    }

    public void HidePath()
    {
        var emission = Trail.emission;
        emission.enabled = false;
    }


    void Update()
    {
        angle += -ClockWise * RotatingSpeed * Time.deltaTime;
        if(angle > 360)
        {
            angle = angle % 360;
        }
        if (angle < 0)
        {
            angle = angle % 360;
            angle += 360;
        }
        //Debug.Log($"Angle: {angle}");

        transform.position = new Vector3(radius * Mathf.Cos(angle / 360f * 2f * Mathf.PI), radius * Mathf.Sin(angle / 360f * 2f * Mathf.PI), 0) + Center;
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevealPath();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HidePath();
        }
    }
}
