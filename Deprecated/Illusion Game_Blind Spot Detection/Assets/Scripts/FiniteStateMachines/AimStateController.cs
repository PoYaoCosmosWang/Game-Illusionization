using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateController : BaseStateController
{
    private float cdTime;
    //Initialize
    private void OnEnable()
    {
        
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cdTime = character.cooldown;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //沒有target-->等待回去idle
        if(character.Target==null)
        {
            return;
        }
        //character.transform.LookAt(character.Target.transform);
        //Debug.Log(character.name + " look at " + character.Target.name);
        Utility.LookAtWithoutYOffest(character.transform,character.Target.transform.position);
        if(cdTime>0)
        {
            cdTime -= Time.deltaTime;
            return;
        }
        //rand decide shoot or not
        float rand = Random.Range(0f, 1f);
        if(rand<character.attackPossibility)
        {
            animator.SetTrigger(BaseStateController.hashShoot);
        }
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("move");
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
