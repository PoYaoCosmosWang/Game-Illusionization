using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLineGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject bar;
    [SerializeField]
    float x;
    [SerializeField]
    float y;

    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(bar, new Vector3(i * 16f + x, y, -3f), Quaternion.identity, transform);
        }
    }

}
