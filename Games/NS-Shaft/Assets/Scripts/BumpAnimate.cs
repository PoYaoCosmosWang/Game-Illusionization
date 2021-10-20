using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpAnimate : MonoBehaviour
{
	private float[] initScale=new float[3];
	Rigidbody m_Rigidbody;
	private float offset = 150f;
	void Start(){
		initScale[0]=this.transform.localScale.x;
		initScale[1]=this.transform.localScale.y;
		initScale[2]=this.transform.localScale.z;
	}


    private void OnCollisionEnter2D(Collision2D other){
    
        if (other.gameObject.CompareTag("Player")){

            StartCoroutine(StartBump(other));
        }
    }

    IEnumerator StartBump(Collision2D other){

        this.transform.localScale=new Vector3(initScale[0],initScale[1]/2,initScale[2]);
        yield return new WaitForSeconds(0.1f/GameManager.GroundMovingSpeed);
        other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,other.gameObject.GetComponent<Rigidbody2D>().gravityScale*5f,0);
        this.transform.localScale=new Vector3(initScale[0],initScale[1],initScale[2]);

        
    }
}
