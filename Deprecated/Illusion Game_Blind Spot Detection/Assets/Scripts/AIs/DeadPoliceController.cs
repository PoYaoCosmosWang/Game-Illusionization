using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPoliceController : MonoBehaviour
{
    
    public WeaponManager weaponManager;

    [SerializeField]
    private Vector3 deadBodyForce;
    [SerializeField]
    private Vector3[] DropWeaponForce;
    [SerializeField]
    private GameObject[] weapons;

    private Rigidbody rig;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        rig.AddForce(deadBodyForce);
        ani.SetTrigger("Die");
        DropWeapons();
    }

    //new way: instantiate around the body
    private void DropWeapons()
    {

        for(int i=0;i<weapons.Length;++i)
        {
            Vector3 position = transform.position + transform.up * 1;
            GameObject weapon = Instantiate(weapons[i],position,Quaternion.identity, weaponManager.transform);
            weapon.GetComponent<Rigidbody>().AddForce(DropWeaponForce[i]);
        }
    }
}
