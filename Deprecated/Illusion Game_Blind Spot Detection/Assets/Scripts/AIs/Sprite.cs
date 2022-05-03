using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sprite : MonoBehaviour
{
    public UnityEvent dieEvent;

    [SerializeField]
    private int _hp;


    
    protected int Hp
    {
        get
        {
            return _hp;
        }
        set
        {

            
            
            _hp =value;
            if(_hp<=0)
            {
                _hp=0;
                Die();
            }
        }
    }

    public void Initialize()
    {
        dieEvent = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Hit(int dmg)
    {
        
        Hp -=dmg;
    }

    protected virtual void Die()
    {
         dieEvent.Invoke();
    }
}
