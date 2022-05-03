using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeaponPoliceManager : MonoBehaviour
{

    public WeaponManager weaponManager;
    
    private DeadPoliceController police;
    // Start is called before the first frame update
    void Start()
    {
        police = GetComponentInChildren<DeadPoliceController>();
        police.weaponManager = weaponManager;
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.K))
        {
            PoliceDie(0.5f);
        }
        */
    }

    public void PoliceDie()
    {
        police.Die();
    }
    
    /*
    public void PoliceDie(float time)
    {
        //police.Die();
        StartCoroutine(PoliceDieCoroutine(time));
    }
    private IEnumerator PoliceDieCoroutine(float time)
    {
        police.Die();
        yield return new WaitForSeconds(time);
        SendMessageUpwards("PoliceDead");
        print("police dead");
    }
    */
}
