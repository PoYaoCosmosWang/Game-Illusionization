using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerspectiveController : MonoBehaviour
{
    private float currentX = 0f;
    private float currentY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
             Cursor.lockState = CursorLockMode.Locked;
        }

        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.Rotate(new Vector3(-currentY, currentX, 0), Space.World);
    }
}
