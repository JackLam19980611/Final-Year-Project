using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRunBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.instance.joystick.Horizontal !=0 || Player.instance.isCrouching) 
        {   
            Player.instance.anim.Play("Idle");
        }
        if (Player.instance.isAttacking) 
        {
            Player.instance.anim.Play("Attack1");
        }
        if (Player.instance.isJumping) 
        {
            Player.instance.anim.Play("Jump");
        }
        if (Player.instance.isFalling)  
        {
            Player.instance.anim.Play("Falling");
        }   
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.instance.isAttacking = false;
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
