using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    
    //public Vector3 playerDestnation;
    private PlayerMoveController player;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMoveController>();
        
    }

    public void StartNavigation(Vector3 destnation)
    {
        player.Destnation = destnation;
        player.arriveEvent.AddListener(PlayerArrived);
    }

    /*public override void Go()
    {
        base.Go();
        player.Destnation = playerDestnation;
        player.arriveEvent.AddListener(StageComplete);
    }
    */
    private void PlayerArrived()
    {
        player.arriveEvent.RemoveListener(PlayerArrived);
        SendMessageUpwards("NavigationEnd");
    }
    /*
    protected override void StageComplete()
    {
        base.StageComplete();
        player.arriveEvent.RemoveListener(StageComplete);
    }
    */
}
