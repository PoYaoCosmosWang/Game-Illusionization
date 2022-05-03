using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : BaseManager
{
    private WeaponManager weaponManager;
    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponentInChildren<WeaponManager>();
    }

    public override void Go()
    {
        base.Go();
        weaponManager.EnableAllWeaponContours(true);
    }
    public void WeaponManagerAction(bool value)
    {
        StageComplete();
        weaponManager.EnableAllWeaponContours(false);
    }

}
