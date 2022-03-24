using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySuccessBehaviour : StateMachineBehaviour
{
    [SerializeField] GameObject parryEffect;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        parryEffect.transform.localScale = new Vector3(Player.instance.transform.localScale.x, parryEffect.transform.localScale.y, parryEffect.transform.localScale.z);
        Instantiate(parryEffect, new Vector3 (Player.instance.transform.position.x+(1*Player.instance.transform.localScale.x), Player.instance.transform.position.y-0.4f, Player.instance.transform.position.z), Quaternion.identity);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.instance.parrySuccessful = false;
        Player.instance.isParrying = false;    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
