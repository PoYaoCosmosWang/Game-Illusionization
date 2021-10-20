using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallKaBehaviour : NoteBehaviour
{
    [SerializeField]
    int playerId;

    bool canBePressed = false;

    void Update()
    {
        KeyCode leftKaKey = playerId == 1 ? Player1GameStatus.leftKaKey : Player2GameStatus.leftKaKey;
        KeyCode rightKaKey = playerId == 1 ? Player1GameStatus.rightKaKey : Player2GameStatus.rightKaKey;

        if (Input.GetKeyDown(leftKaKey) || Input.GetKeyDown(rightKaKey))
        {
            if (canBePressed)
            {
                StartCoroutine(OnHit(playerId));
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HittingArea")
        {
            if(playerId == 1){
                Player1GameStatus.isSmallKa = true;
            }else{
                Player2GameStatus.isSmallKa = true;
            }
            canBePressed = true;
        }
        if (other.tag == "GameController")
        {
            if(playerId == 1){
                Player1GameStatus.UpdateOnMiss();
            }else{
                Player2GameStatus.UpdateOnMiss();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HittingArea")
        {
            if(playerId == 1){
                Player1GameStatus.isSmallKa = false;
            }else{
                Player2GameStatus.isSmallKa = false;
            }
            canBePressed = false;
        }
    }

}
