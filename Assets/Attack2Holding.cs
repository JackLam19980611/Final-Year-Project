using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Holding : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.instance.canJump = true; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.instance.rB.velocity = new Vector2(0,Player.instance.rB.velocity.y);
        if (Player.instance.isAttacking) 
        {
            Player.instance.anim.Play("Attack3");
        }
        /*if (Player.instance.CheckInput()) 
        {
            Player.instance.anim.Play("Idle"); // Exit the current animation (Attack) then it will follow the over side animation flow chart
            Player.instance.canMove = true;
        }*/
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
