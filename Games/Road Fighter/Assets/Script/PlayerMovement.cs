using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private AudioSource footSound;
    Rigidbody rb;
    public float moveSpeed = 10f;
    private void Start()
    {
        setInitPosition();
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }
    void setInitPosition()
    {
        transform.position = new Vector3(-12, 0.5f, 0.5f);
    }
    void move()
    {

        Vector3 newPosition;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)) && transform.position.z < 2.5f)
        {
            footSound.Play();
            newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            gameObject.transform.position = newPosition;
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && transform.position.z > -2.5f)
        {
            footSound.Play();
            newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            gameObject.transform.position = newPosition;
        }
    }


}
