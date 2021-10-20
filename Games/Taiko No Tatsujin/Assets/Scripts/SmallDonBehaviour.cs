using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDonBehaviour : NoteBehaviour
{
    [SerializeField]
    int playerId;

    bool canBePressed = false;

    void Update()
    {
        KeyCode leftDonKey = playerId == 1 ? Player1GameStatus.leftDonKey : Player2GameStatus.leftDonKey;
        KeyCode rightDonKey = playerId == 1 ? Player1GameStatus.rightDonKey : Player2GameStatus.rightDonKey;

        if (Input.GetKeyDown(leftDonKey) || Input.GetKeyDown(rightDonKey))
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
                Player1GameStatus.isSmallDon = true;
            }else{
                Player2GameStatus.isSmallDon = true;
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
                Player1GameStatus.isSmallDon = false;
            }else{
                Player2GameStatus.isSmallDon = false;
            }
            canBePressed = false;
        }
    }
}
