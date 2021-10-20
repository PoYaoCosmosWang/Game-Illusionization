using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class UIControl : MonoBehaviour
{
 
    public void GetValue(int i)
    {
        GroundManager.mode=i;

        /*switch (i)
        {
            case 0: Debug.Log("Default"); break;
            case 1: Debug.Log("without illusion"); break;
            case 2: Debug.Log("illusion"); break;
            case 3: Debug.Log("illusion+fadeoff"); break;
        }*/
    }

}
