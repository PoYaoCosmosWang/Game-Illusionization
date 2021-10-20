using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Target
{
    public GameObject target_object;
    public Color target_color;
    //[Range(1, 10)]
    //public int target_scale = 1;
    //[Range(0f, 360f)]
    //public float target_rotation;
}
public class TargetManager : MonoBehaviour
{

    public Target[] targetElements;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Target targetElement in targetElements)
        {
            targetElement.target_object.GetComponent<MeshRenderer>().material.color = targetElement.target_color;
            //targetElement.target_object.transform.localScale = new Vector3(targetElement.target_scale, targetElement.target_scale, targetElement.target_scale);
            //targetElement.target_object.transform.rotation = Quaternion.Euler(targetElement.target_object.transform.rotation.x, targetElement.target_rotation, targetElement.target_object.transform.rotation.z);
        }
    }
}
