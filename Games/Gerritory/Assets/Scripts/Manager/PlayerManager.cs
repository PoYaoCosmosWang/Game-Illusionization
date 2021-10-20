using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float respawnTime = 3f;
    public static PlayerManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void RespawnPlayer(GameObject player)
    {
        StartCoroutine(RespawnPlayerCoroutine(player));
    }
    private IEnumerator RespawnPlayerCoroutine(GameObject player)
    {
        yield return new WaitForSeconds(respawnTime);
        player.SetActive(true);
        
    }

}
