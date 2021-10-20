using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {


    public static LevelController instance;
    public int CurrentWave = -1;
    public TextMeshProUGUI WaveIndicator;

    //Serializable classes implements
    public EnemyWaves[] enemyWaves; 

    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();

    Camera mainCamera;
    public int Score;
    public TextMeshProUGUI ScoreText;

    public float LevelGeneratorInterval = 10f;
    bool win;
    int numLevelFinished;

    public void GenerateNextWave() {
        CurrentWave++;
        if (!win)
        {
            if (CurrentWave > enemyWaves.Length - 1)
            {
                return;
            }
            StartCoroutine(DisplayWaveInsicator());
        }

    }

    public void CheckWin()
    {
        Debug.Log("Check win");
        numLevelFinished++;
        if (numLevelFinished > enemyWaves.Length -1)
        {
            win = true;
            StartCoroutine(ExitGame());
        }
    }
    public void Win()
    {
        StartCoroutine(ExitGame());
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(3);
        WaveIndicator.text = $"You win the battle!";
        WaveIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        /*
        Debug.Log("Quit App");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
        */
    }

    IEnumerator DisplayWaveInsicator()
    {
        WaveIndicator.text = $"Wave {CurrentWave+1}";
        WaveIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        WaveIndicator.gameObject.SetActive(false);
        Instantiate(enemyWaves[CurrentWave].wave);
        if (CurrentWave < enemyWaves.Length - 1)
        { 
           Instantiate(enemyWaves[CurrentWave+1].wave);
        }

        yield return new WaitForSeconds(LevelGeneratorInterval);

        GenerateNextWave();
    }



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Start()
    {
        win = false;
        numLevelFinished = 0;
        Score = 0;
        ScoreText.text = Score.ToString();
        //ScoreText.alignment = TextAlignmentOptions.TopLeft;
        mainCamera = Camera.main;
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        //for (int i = 0; i < enemyWaves.Length; i++) 
        //{
        //    StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        //}
        GenerateNextWave();
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
    }

    public void AddScore(int s)
    {
        //Debug.Log($"Add score, {s}");
        Score += s;
        ScoreText.text = Score.ToString();

    }

    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            Instantiate(Wave);
    }

    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
