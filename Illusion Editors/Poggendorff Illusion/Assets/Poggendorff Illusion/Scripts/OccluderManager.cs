using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinePoint
{
    public GameObject line_point;
    public bool ShowLinePoint;
    [Range(-25f, 25f)]
    public float line_position;
    [Range(0f, 10f)]
    public float right_length;
    [Range(0f, 10f)]
    public float left_length;
    public Color line_color;
    [Range(0f, 360f)]
    public float line_angle;

}
[System.Serializable]
public class Occluder
{
    [Range(0f, 10f)]
    public float occluder_height;
    [Range(0f, 10f)]
    public float occluder_width;
    public Color occluder_color;
    public GameObject occluder_target;
}
public class OccluderManager : MonoBehaviour
{
    public Occluder occluder;
    [Range(0f, 1f)]
    public float line_width;
    public LinePoint rightLinePoint;
    public LinePoint[] interveneLinePointElements;

    // Update is called once per frame
    void Update()
    {
        AdjustOccluder();
        AdjustInterveneLinePoint();
        AdjustRightLinePoint();
    }
    void AdjustOccluder()
    {
        occluder.occluder_target.transform.localScale = new Vector3(occluder.occluder_width, 1, occluder.occluder_height);
        occluder.occluder_target.GetComponent<MeshRenderer>().material.color = occluder.occluder_color;
    }
    void AdjustInterveneLinePoint()
    {

        foreach (LinePoint lp in interveneLinePointElements)
        {
            lp.line_point.transform.position = new Vector3(lp.line_point.transform.position.x,
                                                                 0,
                                                                 Mathf.Clamp(lp.line_position, -occluder.occluder_height / 2, occluder.occluder_height / 2));
            lp.line_point.transform.rotation = Quaternion.Euler(Vector3.up * lp.line_angle);
            LineRenderer lr = lp.line_point.GetComponent<LineRenderer>();
            if (lp.ShowLinePoint)
            {
                lr.enabled = true;
                Vector3 leftEndPoint = -(lp.line_point.transform.forward) * lp.left_length;
                Vector3 rightEndPoint = (lp.line_point.transform.forward) * lp.right_length;
                lr.SetPosition(0, lp.line_point.transform.position + leftEndPoint);
                lr.SetPosition(1, lp.line_point.transform.position + rightEndPoint);
                lr.startWidth = line_width;
                lr.endWidth = line_width; ;
                lr.material.color = lp.line_color;
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    void AdjustRightLinePoint()
    {
        LinePoint lp = rightLinePoint;
        lp.line_point.transform.position = new Vector3(lp.line_point.transform.position.x,
                                                                 0,
                                                                 Mathf.Clamp(lp.line_position, -occluder.occluder_height / 2, occluder.occluder_height / 2));
        lp.line_point.transform.rotation = Quaternion.Euler(Vector3.up * lp.line_angle);
        LineRenderer lr = lp.line_point.GetComponent<LineRenderer>();
        lp.right_length = 5f;
        if (lp.ShowLinePoint)
        {
            lr.enabled = true;
            Vector3 leftEndPoint = -(lp.line_point.transform.forward) * lp.left_length;
            Vector3 rightEndPoint = (lp.line_point.transform.forward) * lp.right_length;
            lr.SetPosition(0, lp.line_point.transform.position + leftEndPoint);
            lr.SetPosition(1, lp.line_point.transform.position + rightEndPoint);
            lr.startWidth = line_width;
            lr.endWidth = line_width; ;
            lr.material.color = lp.line_color;
        }
        else
        {
            lr.enabled = false;
        }
    }
}

