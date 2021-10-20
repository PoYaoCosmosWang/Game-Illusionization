using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkRangeDetecter : MonoBehaviour
{
    [SerializeField]
    int index;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (index == 1){
                Player1GameStatus.isOk = true;
            } else {
                Player2GameStatus.isOk = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (index == 1){
                Player1GameStatus.isOk = false;
            } else {
                Player2GameStatus.isOk = false;
            }
        }
    }
}
