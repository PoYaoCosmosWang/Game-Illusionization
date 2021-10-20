using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public GameObject GM;
    public float obstacleFromBall;
    public GameObject line1;
    public GameObject line2;
    public float lineWidth;
    public float offset;
    private Color RealLineColor;
    private Color InterveneLineColor;
    private Vector3 lineOffset;
    public int indicatorLength;
    public bool offsetDirection;

    public void ChangeColor()
    {
        RealLineColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        //InterveneLineColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        float realH, realS, realV;
        Color.RGBToHSV(RealLineColor, out realH, out realS, out realV);
        InterveneLineColor = Color.HSVToRGB((realH + 0.5f) % 1, realS, realV);
        offset = offset * (Random.Range(0, 2) * 2 - 1);
    }
    public void move(Vector3 cursorPosition, Vector3 whiteBallPosition)
    {
        GetComponent<MeshRenderer>().enabled = true;
        Vector3 direction = (whiteBallPosition - cursorPosition);
        direction.y = 0;
        direction = direction.normalized;

        CueStickBehavior stick = GM.GetComponent<Game>().cueStick.GetComponent<CueStickBehavior>();
        Vector3 targetPoint = stick.GetCollisionPosition(direction, whiteBallPosition, 0);
        Vector3 reflectDirection = stick.calculateReflectDirection(direction, 0);

        bool reflectWhite; 
        if((targetPoint - whiteBallPosition).magnitude < obstacleFromBall)
        {
            reflectWhite = true;
        }
        else
        {
            reflectWhite = false;
        }
        if (reflectWhite)
        {
            transform.position = targetPoint + reflectDirection * (obstacleFromBall - (targetPoint - whiteBallPosition).magnitude);
            transform.forward = -reflectDirection;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 150, transform.eulerAngles.z);
        }
        else
        {
            transform.position = whiteBallPosition + direction * obstacleFromBall;
            transform.LookAt(whiteBallPosition);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 150, transform.eulerAngles.z);
        }
        // light from obstacle to whiteBall
        LineRenderer ObstacleToWhiteBallLine = GetComponent<LineRenderer>();
        ObstacleToWhiteBallLine.positionCount = 3;
        if (reflectWhite)
        {
            ObstacleToWhiteBallLine.SetPosition(0, transform.position);
            ObstacleToWhiteBallLine.SetPosition(1, targetPoint);
            ObstacleToWhiteBallLine.SetPosition(2, whiteBallPosition);
            stick.ShowObjectPathIndicate(new Material(Shader.Find("Sprites/Default")).color, 0);
        }
        else
        {
            ObstacleToWhiteBallLine.SetPosition(0, transform.position);
            ObstacleToWhiteBallLine.SetPosition(1, whiteBallPosition);
            ObstacleToWhiteBallLine.SetPosition(2, whiteBallPosition);
        }
        ObstacleToWhiteBallLine.startWidth = lineWidth;
        ObstacleToWhiteBallLine.endWidth = lineWidth;
        ObstacleToWhiteBallLine.material = new Material(Shader.Find("Sprites/Default"));

        // RealLine
        stick.GetComponent<CueStickBehavior>().targetPoint[0] = targetPoint;
        stick.realDirection = reflectDirection;
        LineRenderer RealLine = line1.GetComponent<LineRenderer>();
        if (reflectWhite)
        {
            RealLine.positionCount = 2;
            RealLine.SetPosition(0, transform.position);
            RealLine.SetPosition(1, transform.position + reflectDirection * indicatorLength);
        }
        else
        {
            RealLine.positionCount = 3;
            RealLine.SetPosition(0, transform.position);
            RealLine.SetPosition(1, targetPoint);
            RealLine.SetPosition(2, targetPoint + reflectDirection * Mathf.Max(0, (indicatorLength - (targetPoint - whiteBallPosition).magnitude)));
            stick.ShowObjectPathIndicate(RealLineColor, 0);
        }
        RealLine.startWidth = lineWidth;
        RealLine.endWidth = lineWidth;
        RealLine.material = new Material(Shader.Find("Sprites/Default"));
        RealLine.material.color = RealLineColor;

        // InterveneLine
        if (reflectWhite)
        {
            if (offsetDirection)
            {
                lineOffset = new Vector3(-reflectDirection.z, 0, reflectDirection.x).normalized * offset;
            }
            else
            {
                lineOffset = new Vector3(reflectDirection.z, 0, -reflectDirection.x).normalized * offset;
            }
        }
        else
        {
            if (offsetDirection)
            {
                lineOffset = new Vector3(-direction.z, 0, direction.x).normalized * offset;
            }
            else
            {
                lineOffset = new Vector3(direction.z, 0, -direction.x).normalized * offset;
            }
        }
        Vector3 interveneTargetPoint = stick.GetCollisionPosition(direction, transform.position + lineOffset, 1);
        stick.GetComponent<CueStickBehavior>().targetPoint[1] = interveneTargetPoint;
        Vector3 interveneReflectDirection = stick.calculateReflectDirection(direction, 1);
        LineRenderer InterveneLine = line2.GetComponent<LineRenderer>();
        if (reflectWhite)
        {
            InterveneLine.positionCount = 2;
            InterveneLine.SetPosition(0, transform.position + lineOffset);
            InterveneLine.SetPosition(1, transform.position + reflectDirection * indicatorLength + lineOffset);
        }
        else
        {
            InterveneLine.positionCount = 3;
            InterveneLine.SetPosition(0, whiteBallPosition + lineOffset + direction * obstacleFromBall);
            InterveneLine.SetPosition(1, interveneTargetPoint);
            InterveneLine.SetPosition(2, interveneTargetPoint + interveneReflectDirection * Mathf.Max(0, indicatorLength - (interveneTargetPoint - whiteBallPosition - lineOffset).magnitude));
            stick.ShowObjectPathIndicate(InterveneLineColor, 1);
        }
        InterveneLine.startWidth = lineWidth;
        InterveneLine.endWidth = lineWidth;
        InterveneLine.material = new Material(Shader.Find("Sprites/Default"));
        InterveneLine.material.color = InterveneLineColor;

    }
    public void EnterHittingAnimation()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }


}
