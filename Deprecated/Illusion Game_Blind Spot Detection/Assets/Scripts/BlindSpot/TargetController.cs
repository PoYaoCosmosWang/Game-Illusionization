using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    bool isOpen = false;
    [SerializeField]
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            isOpen = !isOpen;
            target.SetActive(isOpen);
        }
    }
}
