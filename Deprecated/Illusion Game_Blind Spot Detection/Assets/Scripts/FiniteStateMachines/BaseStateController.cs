using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for Character
public class BaseStateController : StateMachineBehaviour
{

    protected Character character;

    public static readonly int hashRun = Animator.StringToHash("Running");
    //public static readonly int hashAim = Animator.StringToHash("Aiming");
    public static readonly int hashTarget = Animator.StringToHash("HasTarget");
    public static readonly int hashArrived = Animator.StringToHash("Arrived");
    public static readonly int hashShoot = Animator.StringToHash("Shooting");
    public static readonly int hashShot = Animator.StringToHash("Shot");
    public static readonly int hashHit = Animator.StringToHash("Hit");
    public static readonly int hashDie = Animator.StringToHash("Die");


    //Initialize
    public virtual void Initialize(Character character)
    {
        this.character = character;
    }
    private void OnEnable()
    {
        
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("enter");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
