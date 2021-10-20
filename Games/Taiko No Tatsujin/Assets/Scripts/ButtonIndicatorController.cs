using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndicatorController : MonoBehaviour
{
    SpriteRenderer sR;
    [SerializeField]
    Sprite defaultImage;
    [SerializeField]
    Sprite pressedImage;
    [SerializeField]
    KeyCode keyCode;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            sR.sprite = pressedImage;
        }
        if (Input.GetKeyUp(keyCode))
        {
            sR.sprite = defaultImage;
        }
    }
}
