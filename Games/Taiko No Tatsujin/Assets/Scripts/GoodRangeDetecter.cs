using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodRangeDetecter : MonoBehaviour
{
    [SerializeField]
    int index;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (index == 1){
                Player1GameStatus.isGood = true;
            } else {
                Player2GameStatus.isGood = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (index == 1){
                Player1GameStatus.isGood = false;
            } else {
                Player2GameStatus.isGood = false;
            }
        }
    }
}
