using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    private PlayerController movementController;
    public Color playerColor;
    private int _ownScore = 0;
    //自己實際的分數
    public int OwnScore
    {
        get
        {
            return _ownScore;
        }
        set
        {
            _ownScore = value;
            OnUpdateOwnScore();
        }
    }
    private int _displayScore = 0;
    //最終大家看到的分數，如果沒有分隊那就==score
    public int DisplayScore
    {
        get
        {
            return _displayScore;
        }
        set
        {
            _displayScore = value;
            OnUpdateDisplayScore(_displayScore);
        }
    }
    public UnityAction OnUpdateOwnScore = delegate { };
    public UnityAction<int> OnUpdateDisplayScore = delegate { };
    public UnityAction<bool> OnPlayerEnable = delegate { }; //default value for action
    private void Awake()
    {
        movementController = GetComponent<PlayerController>();
        meshRenderer = GetComponent<MeshRenderer>();
        
    }
    private void Start()
    {
        playerColor = meshRenderer.material.color;
    }


    public void CheckPlayerCollision(Player otherPlayer)
    {
        
        int otherScore = otherPlayer.GetComponent<Player>().DisplayScore;
        if (otherScore < DisplayScore)
        {
            CollideAndKill();
        }
        
    }
    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
        playerColor = mat.color;
    }

    //When two players collide, the one that has more points gets killed, and starts at their original position.
    //The dead player waits for t time.
     private void CollideAndKill(){
        SendMessage("Explode");
        OnPlayerEnable(false);
        gameObject.SetActive(false);
        //自己無法set active自己，所以委託manager
        PlayerManager.instance.RespawnPlayer(this.gameObject);
    }

    public void SetMoveActive(bool active)
    {
        movementController.isActive = active;

    }

    private void OnEnable()
    {
        OnPlayerEnable(true);
    }

    //trash code for fade
    protected MeshRenderer meshRenderer;
    
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    private IEnumerator FadeOutCoroutine()
    {
        float t = 0f;
        while (t <= 1f)
        {

            Color prevColor, afterColor;
            prevColor = playerColor;
            afterColor = playerColor;
            afterColor.a = 0f;
            Color c = Color.Lerp(prevColor, afterColor, t);
            t += Time.deltaTime;
            meshRenderer.material.color = c;
            yield return null;
        }

    }

}
