using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonBehaviour : MonoBehaviour
{
    public void StartIllusionMode(){
        SceneManager.LoadScene("Illusion Game", LoadSceneMode.Single);
    }
    public void StartNoIllusionMode(){
        SceneManager.LoadScene("No Illusion Game", LoadSceneMode.Single);
    }
    public void StartTutorialMode(){
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }
}
