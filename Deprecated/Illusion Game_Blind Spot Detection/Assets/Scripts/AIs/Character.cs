using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Character : Sprite
{
    
    public Vector3 runDestnation;
    private Animator ani;

    //fix animation keep playing
    private bool isDied;

    public Sprite _target;
    public Sprite Target
    {
        get
        {
            return _target;
        }
        set
        {
            
             ani.SetTrigger(BaseStateController.hashTarget);
            _target = value;
           
        }
    }
    public int attack;
    //攻擊機率(0~1)
    public float attackPossibility;
    public float cooldown;

    //用來找尋enemy的radius
    public float searchEnemyRadius;

    //用來偵測敵人
    public int layerMask;

    private void Awake()
    {
        //Initialize(pos);
    }
    void Start()
    {
        
        if (ani!=null) 
            ani.SetBool(BaseStateController.hashRun, true);
        else
        {
            ani = GetComponent<Animator>();
        }
    }

    public void Initialize(Vector3 navPosition)
    {
        base.Initialize();
        this.runDestnation = navPosition;
        InitialzeAnimator();
        
    }

    private void InitialzeAnimator()
    {
        ani = GetComponent<Animator>();
        BaseStateController[] stateBehaviours = ani.GetBehaviours<BaseStateController>();
        foreach(BaseStateController sb in stateBehaviours)
        {
            sb.Initialize(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Hit(int dmg)
    {
        if (isDied) //fix keep playing animation
        {
            return;
        }
        //print("terrorist got hit");
        base.Hit(dmg);
        
    }

    protected override void Die()
    {
        base.Die();
        isDied = true;
        //GetComponent<Collider>().enabled = false;
        ani.SetTrigger(BaseStateController.hashDie);
        this.gameObject.layer = 0;
        //BattleManager.instance.EnemyDie();
    }

    private void TurnOff()
    {
        
    }

}
