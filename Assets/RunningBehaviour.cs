using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        Player.instance.speed = Player.instance.originalSpeed + Player.instance.runningSpeedAdjustment;
        Player.instance.runningAudio.Play();    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.instance.isCrouching || Player.instance.isFalling || Player.instance.isJumping) 
        {
            Player.instance.anim.Play("Idle");
        }
        if (Player.instance.isAttacking) 
        {
            Player.instance.anim.Play("Attack1");
        }
        if (!Player.instance.isRunning) 
        {   
            Player.instance.anim.Play("StopRun");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.instance.isAttacking = false;
        Player.instance.speed = Player.instance.originalSpeed;
        Player.instance.runningAudio.Stop();  
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
