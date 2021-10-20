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
    private float radius;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {

        radius = Vector3.SqrMagnitude(transform.localPosition - Center);
    }

    // Update is called once per frame
    void Update()
    {
        angle += ClockWise * RotatingSpeed * Time.deltaTime;
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
        transform.localPosition = new Vector3(radius * Mathf.Cos(angle / 360f * 2f * Mathf.PI), radius * Mathf.Sin(angle / 360f * 2f * Mathf.PI), 0) + Center;
        //transform.position = new Vector3(radius * Mathf.Cos(angle / 360f * 2f * Mathf.PI), radius * Mathf.Sin(angle / 360f * 2f * Mathf.PI), 0) + Center;
    }
}
