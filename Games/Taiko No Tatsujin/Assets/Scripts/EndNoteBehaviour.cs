using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndNoteBehaviour : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GameController")
        {
            Player1GameStatus.hasStarted = false;
            SceneManager.LoadScene("Settlement", LoadSceneMode.Single);
        }
    }
}
