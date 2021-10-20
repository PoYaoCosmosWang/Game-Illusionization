using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelMove : MonoBehaviour
{
    GameObject player;
    public float smoothing = 20f;
    RectTransform rt;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rt = GetComponent<RectTransform>();
        setInitPosition();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPosition = new Vector2(25, transferPosition(player.transform.position.z));
        rt.anchoredPosition = newPosition;
    }
    float transferPosition(float number)
    {
        // player move from -2.5 to 2.5
        // score panel move from -47 to 108
        return -47.0f + ((number + 2.5f) / 5.0f) * 155.0f;
    }
    void setInitPosition()
    {
        rt.anchoredPosition = new Vector2(25, transferPosition(player.transform.position.z));
    }
}
