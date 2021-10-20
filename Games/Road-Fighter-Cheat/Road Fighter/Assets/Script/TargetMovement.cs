using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : BasicMovingObject
{
    Vector3 newPosition;
    //public float speed = 10f;
    public Material represent;


    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        newPosition = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        gameObject.transform.position = newPosition;
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
