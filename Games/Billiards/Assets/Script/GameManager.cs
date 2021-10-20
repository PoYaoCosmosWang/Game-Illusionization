using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// manager hit ray and control the ball
    /// </summary>
    // Start is called before the first frame update
    private LayerMask tableLayer;
    private RaycastHit layerHit;
    Ray camRay;
    GameObject CueBall;
    LineRenderer pathIndicator;
    public float lineWidth = 0.01f;
    public float hitStrength = 5000f;
    bool isIllusion = false;
    public GameObject PoggedorffItem;

    void Start()
    {
        CueBall = GameObject.FindGameObjectWithTag("CueBall");
        tableLayer = LayerMask.GetMask("Table");
        pathIndicator = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 
        //camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Physics.Raycast(camRay, out layerHit, 100f, tableLayer);

        //if (isIllusion)
        //{
        //    if (CueBall.canShoot)
        //    {
        //        indicateHitPath_with_illusion();
        //    }
        //    else
        //    {
        //        hideHitPath_with_illusion();
        //    }
        //}
        //else
        //{
        //    if (CueBall.canShoot)
        //    {
        //        indicateHitPath();
        //    }
        //    else
        //    {
        //        hideHitPath();
        //    }
        //}

        //if (CueBall.GetComponent<CueBall>().isShoot)
        //{
        //    hideHitPath();
        //}
        //else
        //{
        //    indicateHitPath();

        //    if (Input.GetKey(KeyCode.Space))
        //    {
        //        Vector3 direction = layerHit.point - CueBall.transform.position;
        //        Vector3 horizonDirection = new Vector3(direction.x, 0, direction.z);
        //        CueBall.GetComponent<CueBall>().Shot(horizonDirection.normalized, hitStrength);
        //    }
        //}


    }
    void indicateHitPath()
    {
        pathIndicator.enabled = true;

        if (isIllusion)
        {
            Cursor.visible = false;
            Vector3 itemSpownPoint = CueBall.transform.position + (layerHit.point - CueBall.transform.position) * 0.5f;
            PoggedorffItem.GetComponent<Obstacle>().showPoggendorffIllusion(itemSpownPoint, layerHit.point);

            pathIndicator.SetPosition(0, CueBall.transform.position);
            pathIndicator.SetPosition(1, itemSpownPoint);
        }
        else
        {
            Cursor.visible = true;
            PoggedorffItem.GetComponent<Obstacle>().hidePoggendorffIllusion();
            pathIndicator.SetPosition(0, CueBall.transform.position);
            pathIndicator.SetPosition(1, layerHit.point);

        }
        pathIndicator.startWidth = lineWidth;
        pathIndicator.endWidth = lineWidth;
    }
    void hideHitPath()
    {
        if (isIllusion)
        {
            PoggedorffItem.GetComponent<Obstacle>().hideObstacle();
        }
        else
        {
            PoggedorffItem.GetComponent<Obstacle>().hidePoggendorffIllusion();
            pathIndicator.enabled = false;
        }

    }
    public void TurnPoggendorffEffectOnOFF()
    {
        isIllusion = !isIllusion;
        //PoggedorffItem.GetComponent<Obstacle>().changeColor = true;

    }

}
