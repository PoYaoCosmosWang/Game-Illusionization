using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void StartButton(){
    	GameManager.state="start";
    }

    public void Restart(){
    	GameManager.state="restart";
    }

}
