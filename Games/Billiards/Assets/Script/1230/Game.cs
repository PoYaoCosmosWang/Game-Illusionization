using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public enum GAMESTATE
    {
        START,
        ADJUST,
        ANIMATION,
        WAIT,
        STOP
    }
    public enum PLAYER
    {
        P1,
        P2,
    }
    public Text player1;
    public Text player2;
    public Text p1_Score;
    public Text p2_Score;
    public Button btn_p1_Illusion;
    public Button btn_p2_Illusion;
    public GAMESTATE gameState;
    private GAMESTATE lastState; // use for paused
    public PLAYER currentPlayer = PLAYER.P1;
    public bool isIllusion = false;
    public GameObject cueStick;
    public GameObject obstacle;
    public GameObject whiteBall;
    public GameObject objectBalls;
    private CueStickBehavior cueStickScript;
    private ObstacleBehavior obstacleScript;
    private Vector3 cursorPosition;
    private Vector3 whiteBallPosition;
    public GameObject StartCanvas;
    public GameObject MenuCanvas;
    public GameObject GameCanvas;
    public GameObject EndCanvas;
    public bool useIllusion;

    private LayerMask tableLayer;
    private RaycastHit layerHit;
    Ray camRay;
    // Start is called before the first frame update
    void Start()
    {
        cueStickScript = cueStick.GetComponent<CueStickBehavior>();
        obstacleScript = obstacle.GetComponent<ObstacleBehavior>();
        tableLayer = LayerMask.GetMask("Table");
        gameState = GAMESTATE.WAIT;
        btn_p1_Illusion.interactable = false;
        btn_p2_Illusion.interactable = false;
        StartCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        EndCanvas.SetActive(false);
        btn_p1_Illusion.image.color = Color.gray;
        btn_p2_Illusion.image.color = Color.gray;
        useIllusion = true;
    }

    // Update is called once per frame
    void Update()
    {
        btn_p1_Illusion.gameObject.SetActive(false);
        btn_p2_Illusion.gameObject.SetActive(false);
        if (objectBalls.GetComponentsInChildren<Transform>().Length == 1)
        {
            EndCanvas.SetActive(true);
            GameCanvas.SetActive(false);
            MenuCanvas.SetActive(false);
            StartCanvas.SetActive(false);
        }
        if (currentPlayer == PLAYER.P1)
        {
            player1.color = Color.red;
            player2.color = Color.white;
        }
        else
        {
            player1.color = Color.white;
            player2.color = Color.red;
        }
        switch (gameState)
        {
            case GAMESTATE.START:
                gameState = GAMESTATE.ADJUST;
                break;
            case GAMESTATE.ADJUST:
                fetchInformation();
                AdjustPosition();
                btn_p1_Illusion.interactable = false;
                btn_p2_Illusion.interactable = false;
                if (useIllusion)
                {
                    if (int.Parse(p1_Score.text) - int.Parse(p2_Score.text) >= 1)
                    {
                        if(currentPlayer == PLAYER.P1)
                        {
                            isIllusion = true;
                        }
                        else{
                            isIllusion = false;
                        }
                    }
                    else if (int.Parse(p2_Score.text) - int.Parse(p1_Score.text) >= 1)
                    {
                        if (currentPlayer == PLAYER.P2)
                        {
                            isIllusion = true;
                        }
                        else
                        {
                            isIllusion = false;
                        }
                    }
                    else
                    {
                        isIllusion = false;
                    }
                }
                else
                {
                    isIllusion = false;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    gameState = GAMESTATE.ANIMATION;
                }
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    GamePause();
                }
                //if (Input.GetKeyUp(KeyCode.Alpha1))
                //{
                //    isIllusion = !isIllusion;
                //    if (isIllusion)
                //    {
                //        obstacleScript.ChangeColor();
                //    }
                //}
                break;
            case GAMESTATE.ANIMATION:
                EnterHittingAnimation();
                gameState = GAMESTATE.WAIT;
                break;
            case GAMESTATE.WAIT:
                break;
            case GAMESTATE.STOP:
                // restart
                gameState = GAMESTATE.START;
                break;
        }
        //Debug.Log("game state = " + gameState);
    }


    private void fetchInformation()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(camRay, out layerHit, 100f, tableLayer);
        cursorPosition = new Vector3(layerHit.point.x, 0.125f, layerHit.point.z);
        whiteBallPosition = whiteBall.transform.position;
    }
    private void AdjustPosition()
    {
        cueStickScript.move(cursorPosition, whiteBallPosition);
        if (isIllusion)
        {
            obstacle.SetActive(true);
            obstacleScript.move(cursorPosition, whiteBallPosition);
        }
        else
        {
            obstacle.SetActive(false);
        }
    }
    private void EnterHittingAnimation()
    {
        float back = 1f;
        float front = 4f;
        Vector3 realDirection = cursorPosition - whiteBallPosition;
        realDirection.y = 0;
        //Debug.Log("real direction = " + realDirection.x + " " + realDirection.y + " " + realDirection.z);
        Vector3 direction = realDirection.normalized;
        //Debug.Log("Hitting direction = " + direction.x + " " + direction.y + " " + direction.z);
        cueStickScript.EnterHittingAnimation(cueStick.transform.position + direction * back, cueStick.transform.position - direction * front);

        if (isIllusion)
        {
            obstacleScript.EnterHittingAnimation();
        }
    }
    private void WaitForWhiteBallStop()
    {
        //if (whiteBall.isStop() && !cueStick.isAnimate)
        //{
        //    gameState = GAMESTATE.ADJUST;
        //}
    }
    public void ApplyIllusion()
    {
        obstacleScript.ChangeColor();
    }
    public void GameStart()
    {
        gameState = GAMESTATE.START;
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(true);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene("Demo Game");
    }
    public void GamePause()
    {
        lastState = gameState;
        gameState = GAMESTATE.WAIT;
        GameCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void GameResume()
    {
        gameState = lastState;
        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(true);
    }
}
