using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //serialize for debug
    [SerializeField]
    private BaseManager[] managers;

    private int _stageCnt;
    
    //目前為第幾關
    private int StageCnt
    {
        get
        {
            return _stageCnt;
        }
        set
        {
            _stageCnt = value;
            if(_stageCnt < managers.Length)
            {
                managers[_stageCnt].Go();
            }
            else
            {
                print("all stage complete!");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitManagers();
        Invoke("StartFirstStage", 2f);
    }

    //將所有base manager的level complete監聽
    private void InitManagers()
    {
        managers = GetComponentsInChildren<BaseManager>();
        foreach (BaseManager m in managers)
        {
            m.Initialize();
            m.stageCompleteEvent.AddListener(StageComplete);
        }
    }

    //listen to base managers' LevelCompleteEvent
    private void StageComplete()
    {
        StageCnt++;
    }

    //for debug
    private void StartFirstStage()
    {
        StageCnt = 0;
    }
}
