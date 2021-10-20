using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Vector3 offset = new Vector3(0, -0.09f, 0);
 

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    private void Follow()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.y = 0;
        this.transform.position = playerPosition + offset;
    }
}
