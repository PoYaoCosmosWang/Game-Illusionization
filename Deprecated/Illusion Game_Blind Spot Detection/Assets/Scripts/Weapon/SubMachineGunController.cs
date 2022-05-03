using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMachineGunController : WeaponScript
{
    [SerializeField]
    private GameObject bloodEffect;

    //槍口
	[SerializeField]
	private Transform shootPoint;

    [SerializeField]
    private int attack;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        base.Shoot();
        ShootBullet();
    }

    private void ShootBullet()
	{
		Ray ray = new Ray(shootPoint.position, shootPoint.forward );

		RaycastHit[] hits= Physics.RaycastAll(shootPoint.position, shootPoint.forward);

        foreach(RaycastHit hit in hits)
        {
       
            //改射人的身體，這樣collider才會跟著身體動
			if(hit.collider.CompareTag("Body"))
            {
                Instantiate(bloodEffect,hit.point,Quaternion.identity);
                hit.collider.SendMessageUpwards("Hit",attack);
                return;
				//Debug.Log("SHOOT");
			}
		}
	}
}
