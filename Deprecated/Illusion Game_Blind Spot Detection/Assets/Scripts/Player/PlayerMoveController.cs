using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class PlayerMoveController : MonoBehaviour
{
    private bool arriveDestnation = true;
    private Vector3 _destnation = Vector3.zero;
    public UnityEvent arriveEvent;
    public Vector3 Destnation
    {
        get
        {
            return _destnation;
        }
        set
        {
            arriveDestnation = false;
            _destnation = value;
        }
    }
    private NavMeshAgent nav;
    // Start is called before the first frame update

    private void Awake()
    {
        arriveEvent = new UnityEvent();
    }
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //arriveEvent = new UnityEvent();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(arriveDestnation)
        {
            return;
        }
        if(Utility.GoTo(nav,Destnation))
        {
            arriveDestnation = true;
            //Debug.Log("arrive");
            arriveEvent.Invoke();
            return;
        }
    }
}
