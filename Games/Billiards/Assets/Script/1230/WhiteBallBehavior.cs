using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBallBehavior : MonoBehaviour
{
    public STATE state;
    public GameObject gameManager;
    private Game gameManagerScript;
    private GameObject[] objects;
    private bool flag;
    public bool hit;
    private Vector3 collisionPoint;
    public bool switchPlayer;

    public enum STATE
    {
        START,
        MOVE,
        STOP
    }

    private void Start()
    {
        state = STATE.START;
        gameManagerScript = gameManager.GetComponent<Game>();
        hit = false;
        switchPlayer = true;
    }

    private void Update()
    {
        if (state == STATE.MOVE)
        {
            if (Vector3.Magnitude(GetComponent<Rigidbody>().velocity) <= 0.05f)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                flag = true;
                objects = GameObject.FindGameObjectsWithTag("ObjectBall");
                foreach (GameObject ob in objects)
                {
                    var speed = ob.GetComponent<Rigidbody>().velocity;
                    if (speed.magnitude > 0.01f)
                    {
                        flag = false;
                    }
                    else
                    {
                        if(speed.magnitude <= 0.05f)
                        {
                            ob.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        }
                    }
                }
                if (flag)
                {
                    state = STATE.STOP;
                    gameManagerScript.gameState = Game.GAMESTATE.ADJUST;
                    gameManagerScript.obstacle.GetComponent<ObstacleBehavior>().offsetDirection = (Random.Range(0.0f, 2.0f) >= 1);
                    if (switchPlayer)
                    {
                        if (gameManagerScript.currentPlayer == Game.PLAYER.P1)
                        {
                            gameManagerScript.currentPlayer = Game.PLAYER.P2;
                        }
                        else
                        {
                            gameManagerScript.currentPlayer = Game.PLAYER.P1;
                        }
                    }
                    gameManagerScript.ApplyIllusion();
                    switchPlayer = true;
                    if (gameManagerScript.GetComponent<Game>().isIllusion)
                    {
                        gameManagerScript.isIllusion = false;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hit)
        {
            CueStickBehavior stick = gameManagerScript.GetComponent<Game>().cueStick.GetComponent<CueStickBehavior>();
            collision.rigidbody.velocity = (stick.collisionObjectPosition[0] - stick.targetPoint[0]).normalized * Mathf.Max(GetComponent<Rigidbody>().velocity.magnitude, collision.rigidbody.velocity.magnitude);
            GetComponent<Rigidbody>().velocity = stick.realDirection.normalized * GetComponent<Rigidbody>().velocity.magnitude;
            hit = false;
        }
    }
}
