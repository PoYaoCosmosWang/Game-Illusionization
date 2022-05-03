using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//called in function: LevelComplete
public class StageCompleteEvent: UnityEvent
{

};

//每個關卡都要有base manager，執行Go此關才正式開始
public class BaseManager : MonoBehaviour
{
    public StageCompleteEvent stageCompleteEvent;
    
    //for debug
    [SerializeField]
    private int stage;

    //called by level manager
    public virtual void Initialize()
    {
        stageCompleteEvent = new StageCompleteEvent();
    }
 

    //start this level, called by LevelManager
    public virtual void Go()
    {

    }

    //當關卡結束，請 call this function
    protected virtual void StageComplete()
    {
        print("stage " + stage + " complete");
        stageCompleteEvent.Invoke();
    }
}
