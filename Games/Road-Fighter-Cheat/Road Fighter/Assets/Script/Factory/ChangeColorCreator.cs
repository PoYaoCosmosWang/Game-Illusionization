using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCreator : MonoBehaviour
{
    public GameObject target;

    public void create()
    {
        for (int i = -3; i < 4; i++)
        {
            Vector3 spawnPosition = new Vector3(15f, 0.5f, i);
            GameObject changeColorObject = GameObject.Instantiate(target, spawnPosition, new Quaternion(0, 0, 0, 0)) as GameObject;
        }


    }
}
