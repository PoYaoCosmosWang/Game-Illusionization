using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadRangeDetecter : MonoBehaviour
{
    [SerializeField]
    int index;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (index == 1){
                Player1GameStatus.isBad = true;
            } else {
                Player2GameStatus.isBad = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (index == 1){
                Player1GameStatus.isBad = false;
            } else {
                Player2GameStatus.isBad = false;
            }
        }
    }
}
