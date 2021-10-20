using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoving : MonoBehaviour
{

	private float offset=0.02f;
    void FixedUpdate()
    {

        transform.Translate(0,GameManager.GroundMovingSpeed*offset,0,Space.World);
    }
}
