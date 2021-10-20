using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackground : MonoBehaviour

{

    public Color color = new Color(0.92f, 0.87f, 0.43f);
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
