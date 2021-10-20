using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;
    public float health;
    float maxHealth;
    public Material BloodMat;

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
        maxHealth = health;
        BloodMat.SetFloat("_Progress", health/maxHealth);

    }

    //method for damage proceccing by 'Player'
    public void GetDamage(float damage)   
    {
        Debug.Log($"Get damage {damage}");
        health -= damage;
        if (health < 1) { 
            Destruction();
        }
        BloodMat.SetFloat("_Progress", health / maxHealth);

    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }
}
















