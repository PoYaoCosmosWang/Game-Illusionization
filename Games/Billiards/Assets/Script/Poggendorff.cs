using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Poggendorff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject newLineGameObject;
    public GameObject originalLineGameObject;
    public GameObject GameManager;
    public GameObject whiteBall;
    public GameObject obstacle;
    Vector3 direction;
    public float lineWidth = 0.05f;
    public float offsetdis = 0.1f;
    public float angle;

    RaycastHit layerHit;

    // Update is called once per frame
    void Update()
    {
        layerHit = GameManager.GetComponent<Manager>().layerHit;
        direction = layerHit.point - whiteBall.transform.position;

        if (gameObject.activeSelf)
        {
            showObstacle();
            showLine();
        }
    }
    void showObstacle()
    {
        if (whiteBall.GetComponent<WhiteBallMovement>().isShoot)
        {
            obstacle.GetComponent<MeshRenderer>().enabled = false;

        }
        else
        {
            obstacle.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.position = whiteBall.transform.position + direction.normalized * 4;
            gameObject.transform.LookAt(whiteBall.transform.position);
        }

        //Quaternion newAngle = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y + angle, gameObject.transform.rotation.z);
        //gameObject.transform.rotation = newAngle;
    }
    void showLine()
    {
        if (!whiteBall.GetComponent<WhiteBallMovement>().isShoot)
        {
            LineRenderer originalLine = originalLineGameObject.GetComponent<LineRenderer>();

            originalLine.material.color = Color.red;
            originalLine.startColor = Color.red;
            originalLine.endColor = Color.red;
            originalLine.SetPosition(0, gameObject.transform.position);
            originalLine.SetPosition(1, layerHit.point);
            originalLine.startWidth = lineWidth;
            originalLine.endWidth = lineWidth;

            //set offsets
            Vector3 direction = gameObject.transform.forward.normalized * offsetdis;
            Vector3 offset = new Vector3(direction.z, direction.y, -direction.x);

            LineRenderer newLine = newLineGameObject.GetComponentInChildren<LineRenderer>();
            
            newLine.material.color = Color.blue;
            newLine.startColor = Color.blue;
            newLine.endColor = Color.blue;
            newLine.SetPosition(0, gameObject.transform.position + offset);
            newLine.SetPosition(1, layerHit.point + offset);
            newLine.startWidth = lineWidth;
            newLine.endWidth = lineWidth;
        }

    }

}
