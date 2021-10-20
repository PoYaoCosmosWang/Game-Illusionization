using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllusionControl : MonoBehaviour
{
	
	void Start(){

		GameManager.haveIllusion = false;

	}


	public void GetValue(int i)
    {
        

        switch (i)
        {
            case 0: Debug.Log("non-illusion"); 
					GameManager.haveIllusion = false;
            		break;
            case 1: Debug.Log("illusion"); 
					GameManager.haveIllusion = true;
            		break;
          
        }
    }
}

