using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private WeaponContourController[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        //weapons = GetComponentsInChildren<WeaponContourController>();

    }

    public void EnableAllWeaponContours(bool value)
    {
        weapons = GetComponentsInChildren<WeaponContourController>();

        foreach (WeaponContourController w in weapons)
        {
            w.EnableContour(value);
        }
    }


    public void WeaponAction(bool value)
    {
        SendMessageUpwards("WeaponManagerAction",value);
    }
}
