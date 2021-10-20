using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    private Player[] players;
    private void Start()
    {
        players = GetComponentsInChildren<Player>();
        foreach(Player p in players)
        {
            p.OnUpdateOwnScore += OnPlayerUpdateOwnScore;
        }
    }
    private int CalculateTeamScore()
    {
        int cnt = 0;
        foreach(Player p in players)
        {
            cnt += p.OwnScore;
        }
        return cnt;
    }
    private void OnPlayerUpdateOwnScore()
    {
        int teamScore = CalculateTeamScore();
        foreach(Player p in players)
        {
            p.DisplayScore = teamScore;
        }
    }
}
