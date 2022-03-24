using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBehaviour : StateMachineBehaviour
{   
    [SerializeField] LayerMask ground;
    [SerializeField] GameObject DodgeEffect;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.instance.rB.velocity = new Vector2(Player.instance.dodgeSpeed*Player.instance.transform.localScale.x*Time.fixedDeltaTime, Player.instance.rB.velocity.y);
        DodgeEffect.transform.localScale = new Vector3(Player.instance.transform.localScale.x, DodgeEffect.transform.localScale.y, DodgeEffect.transform.localScale.z);
        Instantiate(DodgeEffect, new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y-1, Player.instance.transform.position.z), Quaternion.identity);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Player.instance.feetCollider.IsTouchingLayers(ground)) 
        {
            Player.instance.anim.Play("Falling");
        }    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
