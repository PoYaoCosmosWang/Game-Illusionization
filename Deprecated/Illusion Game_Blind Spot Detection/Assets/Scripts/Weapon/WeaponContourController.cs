using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContourController : MonoBehaviour
{

    [SerializeField]
    private bool initContainWeapon;

    private bool _containWeapon; //用來enable現在的coutour內的weapon

    public bool ContainWeapon
    {
        get
        {
            return _containWeapon;
        }
        set
        {
            if(value == false )
            {
                //weaponContour.SetActive(true);
                weapon.SetActive(false);
            }
            else
            {
                //weaponContour.SetActive(true);
                weapon.SetActive(true);
            }
            _containWeapon = value;
        }
    }


    [SerializeField]
    private int weaponID;

    //[SerializeField]
    private GameObject weapon;

    //[SerializeField]
    private GameObject weaponContour;

    // Start is called before the first frame update
    void Start()
    {
        InitWeaponObj();
        ContainWeapon = initContainWeapon;
        weaponContour.SetActive(false);
        //temp close ,should be open
    }

    private void InitWeaponObj()
    {
        weapon = transform.GetChild(0).gameObject;
        weaponContour = transform.GetChild(1).gameObject;
    }

    public int PickUp()
    {
        SendMessageUpwards("WeaponAction",true);
        weaponContour.SetActive(false);
        ContainWeapon = false;
        //weapon.SetActive(false);
        return weaponID;
    }

    public void PutDown()
    {
        SendMessageUpwards("WeaponAction",false);
        weaponContour.SetActive(false);
        ContainWeapon = true;
        //weapon.SetActive(true);
    }

    public void EnableContour(bool value)
    {
        weaponContour.SetActive(value);
    }
    

}
