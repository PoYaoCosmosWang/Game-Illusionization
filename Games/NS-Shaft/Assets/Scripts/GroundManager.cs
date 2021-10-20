using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundManager : MonoBehaviour
{
	readonly float leftBorder = -2f;
    readonly float rightBorder = 2f;
    readonly int MAX_GROUND_COUNT = 25;
    readonly int MIN_GROUND_COUNT_UNDER_PLAYER = 10;
    
    public GameObject[] target;

    private float minRangeY = 0.5f;
    private float maxRangeY = 2f;
    public int speedupTime;
    public float MaxSpeed;
      
    private int groundNumber =-1;
    private float spacingY;
    [Range(1,20)] public float singleFloorHeight;
    private List<Transform> grounds;
    public Transform player;
    public Text displayCountFloor;
    public Transform PosY;

    public static int mode;

    private string[] groundtype= new string[] {    "N_N","N_B","N_n","N_L","L_N","L_R","R_N","R_L"};// groundtype_fencetype N=normal or None, L=left ,R=right
    private float[] probabilityillusion= new float[] { 2f,  2f,   3f,   2f,   2f,  5f ,   2f,  3f };
    private float[] probabilityNormal= new float[] { 11f,   4f,   6f, 0f,0f,0f,0f,0f,0f}; //total=20

    private float total_P;

    void Start()
    {
    	total_P=0f;
    	for (int i=0;i<probabilityillusion.Length;i++)
    	   total_P+=probabilityillusion[i];
        grounds = new List<Transform>();
        mode=0;//default
    }

    public void ControlSpawnGround(){
    	int groundCountUNderPlayer = 0;
    	foreach (Transform ground in grounds){
    		if (ground.position.y<player.transform.position.y)
    			groundCountUNderPlayer++;
    	}
    	if (groundCountUNderPlayer<MIN_GROUND_COUNT_UNDER_PLAYER){
    	 	SpawnGround();
    		ControlGroundsCount();
    	} 
    }

    public void ControlGroundsCount(){ 
    	if (grounds.Count > MAX_GROUND_COUNT){
    		Destroy(grounds[0].gameObject);
   			grounds.RemoveAt(0);
    	}
    } 

    public float angle;
 	void SpawnGround(){
        float random_n = Random.Range(0,total_P);
        int index=-1;
        string type="";
        if (mode==0){ //normal
            while(random_n>=0){
                index++;
                random_n-=probabilityNormal[index];
            }
            type=groundtype[index];
        }
        else if (mode==1){
        	while(random_n>=0){
                index++;
                random_n-=probabilityillusion[index];
            }
            type=groundtype[index];
        }

        int groundTypeIndex=0;
        if(type[2]=='N'){
            groundTypeIndex=0;
        }
        else if(type[2]=='B'){
        	groundTypeIndex=1;
        }
        else if(type[2]=='n'){
        	groundTypeIndex=2;
        }
        else if (type[2]=='L'){
            groundTypeIndex=3;
        } 
        else if(type[2]=='R'){
            groundTypeIndex=4;
        }

        GameObject newGround = Instantiate(target[groundTypeIndex]); //Generate Different Ground
        newGround.transform.position = new Vector3 (newGroundPositionX(),newGroundPositionY(),0);

        if (type[0]=='L')
       		newGround.transform.rotation = Quaternion.Euler (0f, 0f, angle);
       	else if (type[0]=='R')
        	newGround.transform.rotation = Quaternion.Euler (0f, 0f, -angle);
        
        grounds.Add(newGround.transform);

        if(type=="N_L"){
        	GameObject newGround2 = Instantiate(target[4]); //Generate Different Ground
        	newGround2.transform.position = new Vector3 (newGroundPositionX(),newGround.transform.position.y-(0.8f+GameManager.GroundMovingSpeed/5),0);
        	grounds.Add(newGround2.transform);
        }

    }

    float newGroundPositionX(){
    	if (grounds.Count==0){
        	return 0;
    	}
    	return Random.Range(leftBorder,rightBorder);
    }

    float newGroundPositionY(){
    	if (grounds.Count==0){
    		return 0;
    	}
    	int lowerIndex = grounds.Count-1;
        spacingY = (GameManager.GroundMovingSpeed/5)+Random.Range(minRangeY,maxRangeY);
    	return grounds[lowerIndex].transform.position.y - spacingY  ; 
    }

    private float prespeed=1.0f;

 	float CountFloor(){

        float count = (PosY.position.y / singleFloorHeight);

        float targetn=(float)Mathf.Min((int)(count/speedupTime),MaxSpeed)/2+1.0f;
        if(GameManager.GroundMovingSpeed< targetn)
           GameManager.GroundMovingSpeed += (targetn-prespeed)*Time.deltaTime*3;
        else
           prespeed=targetn;
         
        return count;

    }
    void DisplayCountFloor(){

        displayCountFloor.text = CountFloor().ToString("0000")+"F";
    }
    // Update is called once per frame
    void Update(){
    	if(GameManager.state=="playing"){
        	ControlSpawnGround();
        	DisplayCountFloor();
    	}
    }
}
