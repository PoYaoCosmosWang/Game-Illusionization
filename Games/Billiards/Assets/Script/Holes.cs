using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holes : MonoBehaviour
{
    public GameObject gm;
    public Text p1_Score;
    public Text p2_Score;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("ObjectBall"))
        {
            if (gm.GetComponent<Game>().currentPlayer == Game.PLAYER.P1)
            {
                p1_Score.text = (int.Parse(p1_Score.text) + 1).ToString();
            }
            else if (gm.GetComponent<Game>().currentPlayer == Game.PLAYER.P2)
            {
                p2_Score.text = (int.Parse(p2_Score.text) + 1).ToString();
            }

            //Debug.Log(other.tag);
            gm.GetComponent<Game>().whiteBall.GetComponent<WhiteBallBehavior>().switchPlayer = false;
            Destroy(other.gameObject);
        }

    }
}
