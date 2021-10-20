using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    public float obstacleSpeed = 10f;
    public GameObject target;
    float timeStamp = 0;
    float interval = 2f;
    private void Update()
    {
        timeStamp += Time.deltaTime;
        if (timeStamp >= interval)
        {
            timeStamp = 0f;
            create();
        }

    }
    public void create()
    {
        int size = Random.Range(2, 5);
        float range = 6 - size;
        Vector3 spawnPosition = new Vector3(15f, 0.5f, (int)Random.Range((-range / 2), (range / 2)));
        GameObject obstacle = GameObject.Instantiate(target, spawnPosition, new Quaternion(0, 0, 0, 0)) as GameObject;
        obstacle.transform.localScale = new Vector3(1, 1, size);
        obstacle.SendMessage("ChangeSpeed", obstacleSpeed * GameManager.levelSpeed);
    }
}
