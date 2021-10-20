using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Startpanel;
    public GameObject Endpanel;
    public GameObject player;
    public static float GroundMovingSpeed;
    public static string state;

    void Start()
    {
     	Startpanel.SetActive(true);
     	state="beforestart";
     	Endpanel.SetActive(false);
        GroundMovingSpeed=0f;   
    }

    // Update is called once per frame
    void Update()
    {   //Debug.Log(state);
        if (state=="beforestart"){
            if (Input.GetKey(KeyCode.Return)){
               state="start";
            }
        }
    	else if (state=="start"){
            Startpanel.SetActive(false);
            player.SetActive(true);
            state="playing";
            GroundMovingSpeed=2f;
    		
    	}
        else if(state=="playing"){
            if(Player.isDead){
                GroundMovingSpeed=0f;
               // Debug.Log("is dead");
            	player.SetActive(false);
            	Endpanel.SetActive(true);
                state="end";
            } 
        }
        else if(state=="end"){
            if (Input.GetKey(KeyCode.Return)){
                   state="restart";
            }
        }
        else if(state=="restart"){
            ReloadScene();
        }
        
        
    }

    public void ReloadScene(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}