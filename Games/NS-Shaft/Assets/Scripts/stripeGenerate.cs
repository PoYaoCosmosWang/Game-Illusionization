using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stripeGenerate : MonoBehaviour
{
   	public GameObject target;
   	[HideInInspector]public float myStrength; 
   	[HideInInspector]public int density=1;
   	[HideInInspector]public float length;
   	[HideInInspector]public int width;
   	[HideInInspector]public bool reverse=false;
   	[HideInInspector]public int angle; 

   	[HideInInspector]public string name="DefaultName"; 

   	private float targetWidth; 
   	private float targetHight; 
   	private float xc,yc,slope_offset,x_offset,y_offset,dist,num,yl,yh;

    void OnDrawGizmos() //draw demo line
    {
    	if (target==null)
    		return;

        targetHight=target.GetComponent<Renderer>().bounds.size.y;

        slope_offset=(targetHight/Mathf.Sin(angle*Mathf.PI/180))*length/2;

        x_offset= slope_offset*Mathf.Cos(angle*Mathf.PI/180);
        y_offset= slope_offset*Mathf.Sin(angle*Mathf.PI/180);
        targetWidth=target.GetComponent<Renderer>().bounds.size.x-0.001f*width-x_offset/length*2;
        xc=target.transform.position.x;
        yc=target.transform.position.y;

        if (reverse){
       		yl=yc+y_offset;
        	yh=yc-y_offset;
        }
        else{
        	yl=yc-y_offset;
        	yh=yc+y_offset;
        }
        num=2*density-1;

        if(num>1)
        	dist=targetWidth/(num-1);
        else
        	dist=0;

        Gizmos.color = new Color(0f, 0f, 0f, 1f);
        for (int i=0;i<width/2;i++){     // for the middle line
       		Gizmos.DrawLine(new Vector3(xc-x_offset+0.001f*i,yl,1), new Vector3(xc+x_offset+0.001f*i,yh,1));
	 		Gizmos.DrawLine(new Vector3(xc-x_offset-0.001f*i,yl,1), new Vector3(xc+x_offset-0.001f*i,yh,1));
	 	}

	    for (int k=1;k<density;k++){
	        for (int i=0;i<width/2;i++){     
	 			Gizmos.DrawLine(new Vector3(xc+k*dist-x_offset+0.001f*i,yl,1), new Vector3(xc+k*dist+x_offset+0.001f*i,yh,1));
	 			Gizmos.DrawLine(new Vector3(xc+k*dist-x_offset-0.001f*i,yl,1), new Vector3(xc+k*dist+x_offset-0.001f*i,yh,1));
	 			Gizmos.DrawLine(new Vector3(xc-k*dist-x_offset+0.001f*i,yl,1), new Vector3(xc-k*dist+x_offset+0.001f*i,yh,1));
	 			Gizmos.DrawLine(new Vector3(xc-k*dist-x_offset-0.001f*i,yl,1), new Vector3(xc-k*dist+x_offset-0.001f*i,yh,1));

	 		}
	 	}
	 	/*
	 	//use for debug -- check boundary
        Gizmos.color = new Color(1f, 1f, 0f, 0.4f); 
        Gizmos.DrawCube(target.transform.position,  new Vector3 (target.GetComponent<Renderer>().bounds.size.x,target.GetComponent<Renderer>().bounds.size.y,1));
    	*/
    }

   
}
