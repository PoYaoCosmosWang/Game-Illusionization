using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovingObject : MonoBehaviour
{
    public float speed = 10f;
    
    public void ChangeSpeed(float speed)
    {
        this.speed = speed;
    }
}
