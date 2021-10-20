using UnityEngine;
using UnityEditor;
using System.Collections;
 
[CustomEditor(typeof(stripeGenerate))]
public class stripeGenerateEditor : Editor
{
	stripeGenerate m_Target;

    private bool foldout=false;
    private bool isreverse=false;
    private float preStrength=-1;
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
    	m_Target = (stripeGenerate)target;
    	m_Target.name =  EditorGUILayout.TextField ("Name", m_Target.name);
    	
    	EditorGUI.BeginDisabledGroup(foldout == true);
 		GUILayout.Label ("Base Setting", EditorStyles.boldLabel);
		m_Target.myStrength = EditorGUILayout.Slider ("Strength", m_Target.myStrength, 0, 1);
		
        if(m_Target.myStrength!=preStrength && !foldout){
			preStrength=m_Target.myStrength;
            isreverse = EditorGUILayout.Toggle("reverse orientation", isreverse);
            m_Target.reverse=isreverse;
			handleStrength();
		}
        EditorGUI.EndDisabledGroup();

		foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, "Advanced Settings"); //Set foldout group
        if (foldout){
            m_Target.density = EditorGUILayout.IntSlider ("density", m_Target.density, 1, 10);
			m_Target.length = EditorGUILayout.Slider ("length", m_Target.length, 0, 5);
			m_Target.width = EditorGUILayout.IntSlider ("width", m_Target.width, 0, 150);
			isreverse = EditorGUILayout.Toggle("reverse orientation", isreverse);
        	m_Target.reverse=isreverse;
       		m_Target.angle = EditorGUILayout.IntSlider ("angle", m_Target.angle, 15, 90);
        }
 		EditorGUILayout.EndFoldoutHeaderGroup();

        if (GUILayout.Button("Generate")){
        	if(m_Target.target==null){
        		Debug.Log("before generate, select the target first");
        	}
			GenerateStripeObject();
		}

        // repaint the scene per frame(handle Gizmos)
        SceneView.RepaintAll();
    }
    private void handleStrength(){ 
    	m_Target.width = (int) (0 + (int)(100f)*m_Target.myStrength);
    	m_Target.length = 0.5f+(1f-0.5f)*m_Target.myStrength ;

    	m_Target.density = (int) (1 + (int)(4f-1f)*m_Target.myStrength);
    	m_Target.angle = (int)(15+ (int)(45f - 15f)*m_Target.myStrength);
    }

    private float targetHight,targetWidth,angle_degree,xc,yc,scale,scalex,orient,num,dist;
    private GameObject newGround;
    public void GenerateStripeObject(){

    	newGround = Instantiate(m_Target.target);
    	newGround.name=m_Target.name;
    	newGround.transform.localPosition=new Vector3(newGround.transform.position.x,newGround.transform.position.y - 0.5f,newGround.transform.position.z);
    	angle_degree = m_Target.angle*Mathf.PI/180 ;
        targetHight=newGround.GetComponent<Renderer>().bounds.size.y/Mathf.Sin(angle_degree)*m_Target.length;
        if (isreverse)
        	orient=0;
       	else
       		orient=180;
        
       	xc = newGround.transform.position.x;
       	yc = newGround.transform.position.y;

    	GameObject ChildObject=Instantiate(Resources.Load<GameObject>("line"));
  		scale=targetHight/ChildObject.GetComponent<Renderer>().bounds.size.y *ChildObject.transform.localScale.y;
        ChildObject.transform.localScale=new Vector3(0.0005f,scale,1);
        ChildObject.transform.rotation = Quaternion.Euler (0f,orient,90-m_Target.angle);
        ChildObject.transform.localPosition=new Vector3(xc,yc,newGround.transform.position.z);
    	ChildObject.transform.parent= newGround.transform;

        for (int i=1;i<m_Target.width/2;i++){     // for the middle line
        	duplicate(xc+0.001f*i);
        	duplicate(xc+0.001f*(-i));
	 	}
	 	targetWidth=newGround.GetComponent<Renderer>().bounds.size.x-0.001f*m_Target.width-targetHight*Mathf.Cos(angle_degree)/m_Target.length;
        num=2*m_Target.density-1;

        if(num>1)
        	dist=targetWidth/(num-1);
        else
        	dist=0;


	    for (int k=1;k<m_Target.density;k++){
	        for (int i=0;i<m_Target.width/2;i++){     
	        	duplicate(xc+k*dist+0.001f*i);
	        	duplicate(xc+k*dist+0.001f*(-i));
	        	duplicate(xc-k*dist+0.001f*i);
	        	duplicate(xc-k*dist+0.001f*(-i));

	 		}
	 	}
    }

    private void duplicate(float x){
    	GameObject ChildObject1=Instantiate(Resources.Load<GameObject>("line"));
	    ChildObject1.transform.localScale=new Vector3(0.001f,scale,1);
	    ChildObject1.transform.rotation = Quaternion.Euler (0f,orient,90-m_Target.angle);
	    ChildObject1.transform.localPosition=new Vector3(x ,yc,newGround.transform.position.z);
	    ChildObject1.transform.parent= newGround.transform;
    }

}
