using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    LineRenderer originLine;
    LineRenderer interveneLine;
    public GameObject[] elements;
    public float offsset = 0.1f;
    public float lineWidth = 0.2f;
    public float obstacleAngle = 30f;
    Color originColor = Color.clear;
    Color interveneColor = Color.clear;
    public bool changeColor = false;
    private void Start()
    {
        originLine = elements[0].GetComponent<LineRenderer>();
        interveneLine = elements[1].GetComponent<LineRenderer>();
        gameObject.SetActive(false);
    }
    public void showPoggendorffIllusion(Vector3 spownPoint, Vector3 intervenePoint)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        showObstacle(spownPoint, intervenePoint);
        showLine(intervenePoint);
    }
    public void hidePoggendorffIllusion()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
    public void hideObstacle()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
    public void showObstacle(Vector3 spownPoint, Vector3 intervenePoint)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.transform.position = spownPoint;
        Vector3 forwardPosition = new Vector3(intervenePoint.x - spownPoint.x, 0f, intervenePoint.z - spownPoint.z);
        transform.forward = Quaternion.Euler(0, 30f, 0) * forwardPosition;


    }

    void showLine(Vector3 intervenePoint)
    {
        if (changeColor)
        {
            originColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            interveneColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            changeColor = false;
        }

        originLine.SetPosition(0, transform.position);
        originLine.SetPosition(1, intervenePoint);
        originLine.startWidth = lineWidth;
        originLine.endWidth = lineWidth;
        originLine.material = new Material(Shader.Find("Sprites/Default"));

        originLine.startColor = originColor;
        originLine.endColor = originColor;
        originLine.material.color = originColor;
        interveneLine.SetPosition(0, transform.position + offsset * transform.forward);

        interveneLine.SetPosition(1, intervenePoint + offsset * transform.forward);
        interveneLine.startWidth = lineWidth;
        interveneLine.endWidth = lineWidth;
        interveneLine.material = new Material(Shader.Find("Sprites/Default"));
        interveneLine.startColor = interveneColor;
        interveneLine.endColor = interveneColor;
        interveneLine.material.color = interveneColor;
    }
}
