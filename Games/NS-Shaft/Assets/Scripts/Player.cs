using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	public float offset;
    public static bool isDead;
	Rigidbody2D playerRigidBody2D;
	readonly float toLeft = -1;
	readonly float toRight = 1;
	readonly float stop = 0;
	float directionX;
    private float prescale;
    private Material playerMat;
    private Color ColorOrigin;
    private Color ColorMove;

    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    private int Life=5;
    public SpriteRenderer HeartRenderer;
    public Sprite[] HeartArray;


    // Start is called before the first frame update
    void Start()
    {
        isDead= false;
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        Life=5;
        playerMat=GetComponent<Renderer>().material;
        ColorOrigin = playerMat.GetColor("_Color");
        ColorMove = new Color(1f, 0f, 0f, 0.3f);
    }

    // Update is called once per frame
 	void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftArrow)){
            spriteRenderer.sprite = spriteArray[1]; 
        	directionX = toLeft;
        }
        else if (Input.GetKey(KeyCode.RightArrow)){
        	directionX = toRight;
            spriteRenderer.sprite = spriteArray[2]; 
        }
        else{
        	directionX = stop;
            spriteRenderer.sprite = spriteArray[0]; 
        }
        transform.Translate(offset*directionX*(1.5f),0,0);

        playerRigidBody2D.gravityScale= 1+(GameManager.GroundMovingSpeed)/5;

    }

    private void OnCollisionExit2D(Collision2D other){
         playerRigidBody2D.velocity = new Vector3(0,playerRigidBody2D.velocity.y,0);
         
    }

    private void OnCollisionEnter2D(Collision2D other){
        playerRigidBody2D.velocity = new Vector3(0,0,0);
        if (other.gameObject.CompareTag("nails")||other.gameObject.CompareTag("ceiling")){
            StartCoroutine(ChangeColor());
            Life=Mathf.Max(0,Life-1);
            if(Life==0)
                isDead = true;
        }
        else if(other.gameObject.CompareTag("top")||other.gameObject.CompareTag("bounce"))
            Life=Mathf.Min(5,Life+1);

        HeartRenderer.sprite = HeartArray[Life]; 
        if(other.gameObject.CompareTag("ceiling")){
            transform.Translate(0,-0.35f,0);
        }

    }
    private void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.CompareTag("top")){
            if (other.gameObject.transform.rotation.z<0){ //R
                transform.Translate(0.03f,0,0);
            }
            else if (other.gameObject.transform.rotation.z>0){ //R
                transform.Translate(-0.03f,0,0);
            }
        }

    }
    IEnumerator ChangeColor(){
        //Debug.Log("colorchange");
        playerMat.SetColor("_Color", ColorMove);
        yield return new WaitForSeconds(0.2f);
        playerMat.SetColor("_Color", ColorOrigin);
        //Debug.Log("color back to normal");
    }

}
