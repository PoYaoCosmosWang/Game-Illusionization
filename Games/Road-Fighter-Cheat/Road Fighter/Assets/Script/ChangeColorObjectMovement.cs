using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorObjectMovement : MonoBehaviour
{
    Vector3 newPosition;
    float speed = 10f;


    // Update is called once per frame
    void FixedUpdate()
    {
        newPosition = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        gameObject.transform.position = newPosition;
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
