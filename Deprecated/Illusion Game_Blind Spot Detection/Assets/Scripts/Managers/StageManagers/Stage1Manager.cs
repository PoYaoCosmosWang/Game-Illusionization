using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : BaseManager
{
    //for loading file
    [SerializeField]
    private string battleFileName;
    private BattleManager battleManager;
    private DropWeaponPoliceManager policeManager;
    // Start is called before the first frame update
    void Start()
    {
        battleManager = GetComponentInChildren<BattleManager>();
        policeManager = GetComponentInChildren<DropWeaponPoliceManager>();
        battleManager.StartBattle(battleFileName);
    }

    public override void Go()
    {
        base.Go();
        policeManager.PoliceDie();
        
    }

    public void BattleEnd()
    {
        StageComplete();
    }
}
