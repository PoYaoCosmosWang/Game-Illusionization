using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueStickBehavior : MonoBehaviour
{
    public GameObject gm;
    private Game gameManagerScript;
    public int stickFromBall; // distance
    public bool isAnimate = false;
    private Vector3 animationBackPosition;
    private Vector3 animationFrontPosition;
    [Range(1, 10)]
    public int indicatorLength;
    [Range(0f, 1f)]
    public float indicatorWidth;
    public float pullSpeed = 1f;
    public float strikeSpeed = 10000f;
    private Vector3 initialStrikeSpeed = new Vector3(23, 0, 0);
    public Vector3[] collisionObjectPosition = new Vector3[2];
    public Vector3[] targetPoint = new Vector3[2];
    private Vector3[] collisionNormal = new Vector3[2];
    public Vector3 realDirection;
    public GameObject[] line = new GameObject[2];
    private bool[] showObject = { false, false };

    public enum STICK_STATE
    {
        ADJUST,
        PULLBACK,
        STRIKE
    }
    private enum COLLISION_TYPE
    {
        BALL,
        WALL
    }
    private enum COLLISION_DIRECTION
    {
        RIGHT = 0,
        LEFT = 1,
        UP = 2,
        DOWN = 3
    }
    public STICK_STATE stick_state = STICK_STATE.ADJUST;
    private COLLISION_TYPE collision_type = COLLISION_TYPE.BALL;
    private void Start()
    {
        stick_state = STICK_STATE.ADJUST;
        GetComponent<BoxCollider>().enabled = false;
    }
    private void FixedUpdate()
    {
        if (stick_state == STICK_STATE.PULLBACK)
        {
            float step = pullSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, animationBackPosition, step);
            if (transform.position == animationBackPosition)
            {
                stick_state = STICK_STATE.STRIKE;
            }
        }
        else if (stick_state == STICK_STATE.STRIKE)
        {
            GetComponent<BoxCollider>().enabled = true;
            float step = strikeSpeed * Time.deltaTime; // calculate distance to move
            Vector3 direction = animationFrontPosition - animationBackPosition;
            GetComponent<Rigidbody>().AddForce(direction.normalized * strikeSpeed);
            initialStrikeSpeed = GetComponent<Rigidbody>().velocity;
        }
    }
    public void setState(string state)
    {
        if (state == "ADJUST") stick_state = STICK_STATE.ADJUST;
        else if (state == "PULLBACK") stick_state = STICK_STATE.PULLBACK;
        else stick_state = STICK_STATE.STRIKE;
    }
    public void move(Vector3 cursorPosition, Vector3 whiteBallPosition)
    {
        line[0].gameObject.SetActive(false);
        line[1].gameObject.SetActive(false);
        Vector3 direction = (cursorPosition - whiteBallPosition);
        direction.y = 0;
        direction = direction.normalized;
        transform.position = whiteBallPosition + direction * stickFromBall;
        transform.LookAt(whiteBallPosition);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90, transform.eulerAngles.y, transform.eulerAngles.z);
        if (!gm.GetComponent<Game>().isIllusion)
        {
            ShowPathIndicate(whiteBallPosition);
        }
        else
        {
            GetComponent<LineRenderer>().enabled = false;
        }
    }
    public void EnterHittingAnimation(Vector3 BackPosition, Vector3 FrontPosition)
    {
        animationBackPosition = BackPosition;
        animationFrontPosition = FrontPosition;
        stick_state = STICK_STATE.PULLBACK;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("CueBall"))
        {
            //Debug.Log("stick speed = " + GetComponent<Rigidbody>().velocity.x + " " + GetComponent<Rigidbody>().velocity.y + " " + GetComponent<Rigidbody>().velocity.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            stick_state = STICK_STATE.ADJUST;
            GetComponent<BoxCollider>().enabled = false;
            collision.transform.GetComponent<WhiteBallBehavior>().state = WhiteBallBehavior.STATE.MOVE;
            collision.rigidbody.velocity = initialStrikeSpeed;
            collision.transform.GetComponent<WhiteBallBehavior>().hit = true;
            //Debug.Log("speed = " + collision.transform.GetComponent<Rigidbody>().velocity.x + " " + collision.transform.GetComponent<Rigidbody>().velocity.y + " " + collision.transform.GetComponent<Rigidbody>().velocity.z);
        }
    }
    void ShowPathIndicate(Vector3 whiteBallPosition)
    {
        GetComponent<LineRenderer>().enabled = true;
        Vector3 direction = (whiteBallPosition - transform.position).normalized;
        targetPoint[0] = GetCollisionPosition(direction, whiteBallPosition, 0);
        Vector3 reflectDirection = calculateReflectDirection(direction, 0);
        realDirection = reflectDirection;
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = 3;
        lr.SetPosition(0, whiteBallPosition);
        lr.SetPosition(1, targetPoint[0]);
        lr.SetPosition(2, targetPoint[0] + reflectDirection * (indicatorLength - (targetPoint[0] - whiteBallPosition).magnitude));
        lr.startWidth = indicatorWidth;
        lr.endWidth = indicatorWidth;
        lr.material.color = Color.white;
        ShowObjectPathIndicate(lr.material.color, 0);
    }

    public void ShowObjectPathIndicate(Color lineColor, int real)
    {
        LineRenderer objectLine = line[real].GetComponent<LineRenderer>();
        Vector3 objectDirection = (collisionObjectPosition[real] - targetPoint[real]);
        if (showObject[real])
        {
            line[real].gameObject.SetActive(true);
            objectLine.SetPosition(0, collisionObjectPosition[real]);
            objectLine.SetPosition(1, collisionObjectPosition[real] + objectDirection.normalized);
            objectLine.startWidth = indicatorWidth;
            objectLine.endWidth = indicatorWidth;
            objectLine.material = new Material(Shader.Find("Sprites/Default"));
            objectLine.material.color = lineColor;
            Debug.DrawLine(collisionObjectPosition[real], collisionObjectPosition[real] + objectDirection.normalized, Color.white);
        }
        else
        {
            line[real].gameObject.SetActive(false);
        }
    }

    public Vector3 GetCollisionPosition(Vector3 direction, Vector3 whiteBallPosition, int real)
    {
        Vector3 nearestCollisionPoint = new Vector3(0, 100, 0);
        gameManagerScript = gm.GetComponent<Game>();
        GameObject whiteBall = gameManagerScript.whiteBall;
        RaycastHit hit;
        float radius = whiteBall.GetComponent<SphereCollider>().radius * whiteBall.GetComponent<Transform>().localScale.x;
        if (Physics.SphereCast(whiteBallPosition, radius, direction, out hit))
        {
            //Debug.Log("Hit: " + hit.point);
            if(hit.collider.tag == "ObjectBall")
            {
                nearestCollisionPoint = hit.point + radius * hit.normal;
                collisionObjectPosition[real] = hit.transform.position;
                collisionNormal[real] = new Vector3(-hit.normal.x, -hit.normal.y, -hit.normal.z);
                collision_type = COLLISION_TYPE.BALL;
            }
            if(hit.collider.tag == "billiard")
            {
                COLLISION_DIRECTION collisionDirection = COLLISION_DIRECTION.RIGHT;
                nearestCollisionPoint = hit.point + radius * hit.normal;
                if(hit.point.x > 4.0)
                {
                    collisionDirection = COLLISION_DIRECTION.RIGHT;
                }
                else if (hit.point.x < -4.0)
                {
                    collisionDirection = COLLISION_DIRECTION.LEFT;
                }
                else if(hit.point.z > 1.85)
                {
                    collisionDirection = COLLISION_DIRECTION.UP;
                }
                else if(hit.point.z < -1.84)
                {
                    collisionDirection = COLLISION_DIRECTION.DOWN;
                }
                collisionObjectPosition[real] = new Vector3(0, (int)collisionDirection * 10, 0);
                collision_type = COLLISION_TYPE.WALL;
            }
        }
        showObject[real] = true;
        if ((nearestCollisionPoint - whiteBallPosition).magnitude > indicatorLength)
        {
            showObject[real] = false;
            nearestCollisionPoint = whiteBallPosition + direction * indicatorLength;
        }
        if(collision_type == COLLISION_TYPE.WALL)
        {
            showObject[real] = false;
        }
        //Debug.Log("nearest = " + nearestCollisionPoint);
        return nearestCollisionPoint;
    }

    public Vector3 calculateReflectDirection(Vector3 direction, int real)
    {
        switch (collision_type)
        {
            case COLLISION_TYPE.BALL:
                Vector3 objectDirection = (collisionObjectPosition[real] - targetPoint[real]);
                //Debug.Log(direction + " " + Vector3.Reflect(direction, collisionNormal[real]));
                //Debug.DrawLine(collisionObjectPosition[real], collisionObjectPosition[real] + Vector3.Reflect(direction, collisionNormal[real]) * indicatorLength);
                Vector3 reflectDirection = Vector3.Reflect(direction, collisionNormal[real]).normalized;
                //Debug.DrawLine(collisionObjectPosition[real], collisionObjectPosition[real] + direction - (collisionObjectPosition[real] - targetPoint[real]).normalized);
                Vector3 normalDirection;
                if (Vector3.Cross(objectDirection, direction).y > 0)
                {
                    normalDirection = new Vector3(objectDirection.z, 0, -objectDirection.x).normalized;
                }
                else
                {
                    normalDirection = new Vector3(-objectDirection.z, 0, objectDirection.x).normalized;
                }
                if(Vector3.Angle(objectDirection, direction) < 10)
                {
                    return Vector3.Reflect(direction, collisionNormal[real]).normalized;
                }
                else
                {
                    return (reflectDirection + normalDirection).normalized;
                }
            case COLLISION_TYPE.WALL:
                switch(collisionObjectPosition[real].y)
                {
                    case 0:
                        return Vector3.Reflect(direction, new Vector3(1, 0, 0));
                    case 10:
                        return Vector3.Reflect(direction, new Vector3(-1, 0, 0));
                    case 20:
                        return Vector3.Reflect(direction, new Vector3(0, 0, 1));
                    case 30:
                        return Vector3.Reflect(direction, new Vector3(0, 0, -1));
                }
                break;
        }
        return new Vector3(0, 0, 0);
    }

    private void OnDrawGizmos()
    {
        gameManagerScript = gm.GetComponent<Game>();
        GameObject whiteBall = gameManagerScript.whiteBall;
        float radius = whiteBall.GetComponent<SphereCollider>().radius * whiteBall.GetComponent<Transform>().localScale.x;
        Gizmos.DrawSphere(targetPoint[0], radius);
    }
}
