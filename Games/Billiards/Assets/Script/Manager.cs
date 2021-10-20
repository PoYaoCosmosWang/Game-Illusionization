using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool Poggendorff = false;
    public GameObject PoggendorffItem;
    public GameObject whiteBall;
    // Start is called before the first frame update
    public RaycastHit layerHit;
    Ray camRay;
    LayerMask layer;

    // Update is called once per frame
    private void Start()
    {
        layer = LayerMask.GetMask("Table");
    }
    void Update()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(camRay, out layerHit, 100f, layer);
        if (Poggendorff)
        {
            Cursor.visible = false;
            if (!PoggendorffItem.activeSelf)
            {
                PoggendorffItem.SetActive(true);
            }
        }
        else
        {
            Cursor.visible = true;
            if (PoggendorffItem.activeSelf)
            {
                PoggendorffItem.SetActive(false);
            }


        }
    }
}
