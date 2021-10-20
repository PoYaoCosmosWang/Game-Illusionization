using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetScroller : MonoBehaviour
{
    [SerializeField]
    AudioSource bgm;
    [SerializeField]
    float tempo;

    // Start is called before the first frame update
    void Start()
    {
        tempo /= 60f;
        bgm.Play();
        Player1GameStatus.hasStarted = true;
        Player2GameStatus.hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player1GameStatus.hasStarted) { return; }

        transform.position -= new Vector3(tempo * Time.deltaTime, 0f, 0f);
    }
}
