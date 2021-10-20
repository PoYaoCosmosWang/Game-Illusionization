using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonBehaviour : MonoBehaviour
{
    public void ReturnLobby(){
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
        Player1GameStatus.Reset();
    }
}
