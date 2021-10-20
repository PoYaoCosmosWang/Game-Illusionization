using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WhiteBallMovement : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3 direction;
    Rigidbody rigidBody;
    public float forceStrength = 100f;
    public GameObject GameManager;
    public GameObject Poggendorff;
    float lineWidth = 0.1f;
    public bool isShoot = false;
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rigidBody = gameObject.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (Vector3.Magnitude(rigidBody.velocity) > 0.5f)
        {
            isShoot = true;
        }
        else
        {
            isShoot = false;
            indicateDirection();
            if (Input.GetButton("Fire1"))
            {
                shot();
            }
        }



    }
    void indicateDirection()
    {
        RaycastHit layerHit = GameManager.GetComponent<Manager>().layerHit;

        direction = layerHit.point - transform.position;
        lineRenderer.material.color = Color.gray;
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.SetPosition(0, transform.position);
        if (GameManager.GetComponent<Manager>().Poggendorff)
        {
            lineRenderer.SetPosition(1, Poggendorff.transform.position);
        }
        else
        {
            lineRenderer.SetPosition(1, layerHit.point);
        }



    }

    void shot()
    {

        rigidBody.AddForce(direction * forceStrength);
    }

    void PoggendorffIllusion()
    {

    }
}
