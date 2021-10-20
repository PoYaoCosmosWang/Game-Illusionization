using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string prefixFileName = "MapInfo/Sakai";

    [SerializeField]
    private Text gameDisplayText;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Map map;
    [SerializeField]
    private Timer totalTimer;
    [SerializeField]
    private int totalTime;


    private void Start()
    {
        //players = GameObject.FindObjectsOfType<Player>()as Player[];
        map.onWinGame += OnWinGame;
        StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow()
    {
        yield return StartCoroutine(GameStartCountDownCoroutine());
        yield return new WaitForSeconds(0.2f);
        gameDisplayText.gameObject.SetActive(false);
        SetPlayersMovementActive(true);
        TimerActive(true,totalTime);
    }


    private IEnumerator GameStartCountDownCoroutine()
    {
        SetPlayersMovementActive(false);
        gameDisplayText.gameObject.SetActive(true);
        int time = 3;
        while (time > 0)
        {
            gameDisplayText.text = time.ToString() + "!";
            yield return new WaitForSeconds(1f);
            time--;
        }
        gameDisplayText.text = "GO!";
    }

    public void OnWinGame()
    {
        //stop and fade all player
        SetPlayersMovementActive(false);
        FadePlayers();
        //show win
        
        gameDisplayText.text = "WIN!";
        gameDisplayText.gameObject.SetActive(true);
        TimerActive(false);
    }

    private void FadePlayers()
    {
        foreach(Player p in players)
        {
            p.FadeOut();
        }
    }

    public void OnTimeUp()
    {
        //stop all player
        SetPlayersMovementActive(false);
        //show lose
        
        /*
        gameDisplayText.text = "LOSE";
        gameDisplayText.gameObject.SetActive(true);
        */

        
        int idx = CalculateWinner();
        gameDisplayText.text = "Player " + (idx + 1) + " Wins!";
        gameDisplayText.gameObject.SetActive(true);
       
    }

    private void SetPlayersMovementActive(bool active)
    {
        foreach (Player p in players)
        {
            p.SetMoveActive(active);
        }
    }
    private int CalculateWinner()
    {
        int maxScore = -1;
        int idx = -1;
        for(int i=0;i<players.Length;++i)
        {
            print("player " + i + " score = " + players[i].DisplayScore);
            if(players[i].DisplayScore > maxScore)
            {
                maxScore = players[i].DisplayScore;
                idx = i;
            }
        }
        return idx;
    }
    private void TimerActive(bool active,int startTime=0)
    {
        if(active)
        {
            totalTimer.CountDownActive(true,startTime);
            totalTimer.timeUpAction += OnTimeUp;
        }
        else
        {
            totalTimer.CountDownActive(false);
            totalTimer.timeUpAction -= OnTimeUp;
        }

    }

    

}
