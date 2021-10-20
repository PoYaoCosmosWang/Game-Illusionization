using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Random = UnityEngine.Random;
/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public float health;
    public int Value;
    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    #endregion
        
    public event Action OnDestruction;

    private void Start()
    {
        StartCoroutine(ActivateShooting());

    }
    private void Update()
    {
        
    }


    //coroutine making a shot
    IEnumerator ActivateShooting() 
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(shotTimeMin, shotTimeMax)); 

            if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
            {
                Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(float damage) 
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
        {
            Destruction();
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
            LevelController.instance.AddScore(Value);
        }


    }

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1f);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //OnDestruction.Invoke();

    }
}
