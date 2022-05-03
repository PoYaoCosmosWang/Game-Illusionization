using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Manager : BaseManager
{
    //for loading file
    [SerializeField]
    private string battleFileName;
    private BattleManager battleManager;
    private WeaponManager weaponManager;
    private DropWeaponPoliceManager dropWeaponPoliceManager;
    [SerializeField]
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        battleManager = GetComponentInChildren<BattleManager>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        dropWeaponPoliceManager = GetComponentInChildren<DropWeaponPoliceManager>();
        battleManager.StartBattle(battleFileName);
    }

    public override void Go()
    {
        base.Go();
        CoroutineUtility.GetInstance().Do()
        .Then(dropWeaponPoliceManager.PoliceDie)
        .Wait(1.5f)
        .Then(()=>weaponManager.EnableAllWeaponContours(true))
        .Go();
        
    }
    

    public void BattleEnd()
    {
        StageComplete();
        weaponManager.EnableAllWeaponContours(false);
    }

    public void WeaponManagerAction(bool value)
    {
        //do nothing
    }
}
