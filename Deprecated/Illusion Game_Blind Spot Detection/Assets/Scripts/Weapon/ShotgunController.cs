using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController  : WeaponScript
{
    [SerializeField]
    private GameObject bloodEffect;

    [SerializeField]
    private float radius;
    [SerializeField]
    private float depth;
   
    [SerializeField]
    private int attack;

    [SerializeField]
    private Transform shootPoint;
    // Start is called before the first frame update
     protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //ShootCone();
    }

    protected override void Shoot()
    {
        base.Shoot();
        ShootCone();
    }
    private void ShootCone()
	{
		RaycastHit[] hits = Physics.SphereCastAll(shootPoint.position, radius, shootPoint.transform.forward, depth);
        for(int i=0;i<hits.Length;++i)
        {
           //Debug.LogError(hits[i].collider.name);
           //if ( hits[i].collider.CompareTag("Terrorist") )
			if(hits[i].collider.CompareTag("Body"))
            {
                Instantiate(bloodEffect,hits[i].point,Quaternion.identity);
                float percentage = 1 - hits[i].distance/depth;
                print("attack " + percentage);
                //hits[i].collider.SendMessage("Hit",attack*percentage);
				hits[i].collider.SendMessageUpwards("Hit",attack*percentage);
                //Debug.Log("SHOOT");
			}
        }

	}
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(shootPoint.position,shootPoint.position+shootPoint.forward*depth);
        Gizmos.DrawWireSphere(shootPoint.position+shootPoint.forward*depth,radius);
    }
}
