using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_animate : MonoBehaviour
{

	private bool start_animate=false;
    private int count;

	private float transparency=1f;
	private float fadeSpeed=1.3f;

    private GameObject Child;
    private Material curMat;
  
    void Update(){
    	if (start_animate && transparency>0){
    		transparency-=fadeSpeed*Time.deltaTime;
    		if (transparency<0)
    			transparency=0;
            if (gameObject.transform.childCount<=0)
                start_animate=false;
    		for (int i=1;i< gameObject.transform.childCount;i++){
                Child=gameObject.transform.GetChild(i).gameObject;
                curMat = Child.GetComponent<Renderer>().material;

    			ChangeAlpha(curMat,transparency);
    		}	
    	}
    }

    private void ChangeAlpha(Material currentMat, float alphaVal)
    {

        Color newColor = new Color(1, 1, 1, alphaVal);
        currentMat.SetColor("_Color", newColor);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.name=="player" ){
        	start_animate=true;   			
        }
    }
}
