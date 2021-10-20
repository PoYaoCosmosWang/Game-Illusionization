using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool haveIllusion = false;
    public enum STATE
    {
        INIT,
        NORMAL,
        NORMAL_WAIT,
        TRANSITION,
        ILLUSION,
        ILLUSION_WAIT,
        PAUSE,
    }
    public STATE state;
    private STATE lastState;
    public GameObject normalCubeCreator;
    public GameObject obstacleCreator;
    public GameObject stripeCreator;
    public GameObject changeColorCreator;
    List<GameObject> creators = new List<GameObject>();
    public Material[] materials;
    public Timer timer;
    public GameObject mainCamera;
    public GameObject illusionCamera;
    [SerializeField]
    private Camera changeCamera;
    private float timeStamp = 0f;
    public int _level = 1;
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            if(_level<value)
            {
                levelSpeed *= levelSpeedCoefficient;
            }
            _level = value;
        }
    }
    public float cameraMovingSpeed;
    public GameObject StartCanvas;
    public GameObject GameCanvas;
    public GameObject MenuCanvas;
    public GameObject EndCanvas;
    public Text scorePanel;
    public Text endResult;

    #region for adjusting object speed
    //每過一關就讓他更高
    public static float levelSpeed = 1f;
    public float levelSpeedCoefficient = 1.2f;
    #endregion

    public void Start()
    {
        normalCubeCreator.SetActive(false);
        obstacleCreator.SetActive(false);
        //stripeCreator.SetActive(false);
        changeColorCreator.SetActive(false);
        creators.Add(normalCubeCreator);
        creators.Add(obstacleCreator);
        creators.Add(stripeCreator);
        creators.Add(changeColorCreator);
        illusionCamera.SetActive(false);
        StartCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
        EndCanvas.SetActive(false);
        state = STATE.INIT;
        stripeCreator.GetComponent<StripeCreator>().Create();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            stripeCreator.GetComponent<StripeCreator>().Create2();
            stripeCreator.GetComponent<StripeCreator>().Create();

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(ChangePOVCoroutine());
        }
        if  (int.Parse(scorePanel.text) <0 )
        {
            endResult.text="You Dead!";
            GameEnd();
        }
        switch (state)
        {
            case STATE.INIT:
                CloseAllCreator();
                Level = 1;
                break;
            case STATE.NORMAL:
                switch (Level)
                {
                    case 1:
                        Level1();
                        break;
                    case 2:
                        Level2();
                        break;
                }
                state = STATE.NORMAL_WAIT;
                break;
            case STATE.NORMAL_WAIT:
                timeStamp += Time.deltaTime;
                if (Level < 2)
                {
                    if (timeStamp > 10f)
                    {
                        state = STATE.NORMAL;
                        Level += 1;
                        timeStamp = 0;
                    }
                }
                else
                {
                    if (timeStamp > 10f)
                    {

                        state = STATE.TRANSITION;
                        Level = 1;
                        timeStamp = 0;
                    }
                }


                break;
            case STATE.TRANSITION:
                ChangePOV();
                break;
            case STATE.ILLUSION:
                switch (Level)
                {
                    case 1:
                        Level1();
                        break;
                    case 2:
                        Level2();
                        break;
                    case 3:
                        Level3();
                        break;
                    case 4:
                        Level4();
                        break;
                    case 5:
                        Level5();
                        break;
                    case 6:
                        Level6();
                        break;
                }
                state = STATE.ILLUSION_WAIT;
                break;
            case STATE.ILLUSION_WAIT:
                timeStamp += Time.deltaTime;
                if (Level < 6)
                {
                    if (timeStamp > 10f)
                    {
                        Level += 1;
                        state = STATE.ILLUSION;
                        timeStamp = 0;
                    }
                }
                else
                {
                    if ((int.Parse(scorePanel.text) >= 20))
                    {
                        endResult.text="You Win!";
                        GameEnd();
                    }
                }
                break;
            case STATE.PAUSE:
                GamePause();
                break;
        }

    }
    public void GameStart()
    {
        levelSpeed = 1f;
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        timer.StartTimer();
        MenuCanvas.SetActive(false);
        EndCanvas.SetActive(false);
        state = STATE.NORMAL;
    }
    public void GamePause()
    {
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
        EndCanvas.SetActive(false);
        if (state != STATE.PAUSE)
        {
            lastState = state;
            state = STATE.PAUSE;
        }
    }
    public void GameResume()
    {
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        EndCanvas.SetActive(false);
        if (state == STATE.PAUSE)
        {
            state = lastState;
        }
    }
    public void GameEnd()
    {
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
        EndCanvas.SetActive(true);
        CloseAllCreator();

    }
    public void GameRestart()
    {
        SceneManager.LoadScene("Demo Game");
    }
    public void ChangePOV()
    {
        
        Vector3 illusionPosition = new Vector3(0f, 10f, 0f);
        Quaternion illusionRotation = Quaternion.Euler(90, 0, 0);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, illusionPosition, cameraMovingSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, illusionRotation, cameraMovingSpeed * Time.deltaTime);
        if (Vector3.Distance(mainCamera.transform.position, illusionPosition) < 0.1f)
        {
            mainCamera.GetComponent<Camera>().orthographic = true;
            mainCamera.transform.position = illusionPosition;
            mainCamera.transform.rotation = illusionRotation;
            mainCamera.GetComponent<Camera>().orthographicSize = 7.6f;
            state = STATE.ILLUSION;
        }
    }
    private IEnumerator ChangePOVCoroutine()
    {
        changeCamera.orthographic = false;
        Vector3 originPosition = new Vector3(0f, 10f, 0f);
        Quaternion originRotation = Quaternion.Euler(90, 0, 0);
        Vector3 afterPosition = new Vector3(-13.87f, 2.23f, 0f);
        Quaternion afterRotation = Quaternion.Euler(24, 90, 0);
        while(Vector3.Distance(changeCamera.transform.position, afterPosition)>0.1f)
        {
            changeCamera.transform.position = Vector3.Lerp(changeCamera.transform.position, afterPosition, cameraMovingSpeed * Time.deltaTime);
            changeCamera.transform.rotation = Quaternion.Slerp(changeCamera.transform.rotation, afterRotation, cameraMovingSpeed * Time.deltaTime);
            yield return null;
        }
        //if (Vector3.Distance(changeCamera.transform.position, illusionPosition) < 0.1f)
        {
            //changeCamera.GetComponent<Camera>().orthographic = true;
            //changeCamera.transform.position = illusionPosition;
            //changeCamera.transform.rotation = illusionRotation;
            //changeCamera.GetComponent<Camera>().orthographicSize = 7.6f;
            //state = STATE.ILLUSION;
        }
    }
    public void ChangeBackPOV()
    {
        Vector3 initPosittion = new Vector3(-12.6f, 6.1f, 0);
        Quaternion initRotation = Quaternion.Euler(50f, 90f, 0f);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, initPosittion, cameraMovingSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, initRotation, cameraMovingSpeed * Time.deltaTime);
        if (Vector3.Distance(mainCamera.transform.position, illusionCamera.transform.position) < 0.1f)
        {
            mainCamera.GetComponent<Camera>().orthographic = false;
            mainCamera.transform.position = initPosittion;
            mainCamera.transform.rotation = initRotation;
            state = STATE.INIT;
        }
    }
    public void CloseAllCreator()
    {
        foreach (GameObject creator in creators)
        {
            normalCubeCreator.SetActive(false);
        }
    }
    public void Level1()
    {
        // normal
        CloseAllCreator();
        normalCubeCreator.SetActive(true);
    }

    public void Level2()
    {
        // normal cube and obstacle
        CloseAllCreator();
        normalCubeCreator.SetActive(true);
        obstacleCreator.SetActive(true);
    }
    public void Level3()
    {
        // normal cube, obstacle, stripe
        CloseAllCreator();
        normalCubeCreator.SetActive(true);
        obstacleCreator.SetActive(true);
        if(haveIllusion)
        {
            stripeCreator.SetActive(true);
            stripeCreator.GetComponent<StripeCreator>().Create();
        }
    }
    public void Level4()
    {
        // normal cube, obstacle, stripe, change color
        CloseAllCreator();

        changeColorCreator.SetActive(true);
        changeColorCreator.GetComponent<ChangeColorCreator>().create();
        Invoke("Level1", 10f);
        Invoke("Level2", 10f);
        Invoke("Level3", 10f);
    }
    public void Level5()
    {
        CloseAllCreator();
        if (haveIllusion)
        {
            stripeCreator.SetActive(true);
        }
        normalCubeCreator.SetActive(true);
        normalCubeCreator.GetComponent<GoldCoinCreator>().Create();
        //StartCoroutine(stripeCreator.GetComponent<StripeCreator>().create(10, 60, 5));

    }
    public void Level6()
    {
        CloseAllCreator();
        if(haveIllusion)
        {
            stripeCreator.SetActive(true);
        }
        normalCubeCreator.SetActive(true);
        normalCubeCreator.GetComponent<GoldCoinCreator>().Create();
        StartCoroutine(stripeCreator.GetComponent<StripeCreator>().CreateTranspanrentSprite());
    }

}
