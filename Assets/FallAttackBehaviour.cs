using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAttackBehaviour : StateMachineBehaviour
{   
    [SerializeField] LayerMask ground;
    [SerializeField] GameObject FAD;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.instance.canMove = false;
        Player.instance.canJump = false;
        Player.instance.changeSide = false;
        Player.instance.rB.velocity = new Vector2(0, -600*Time.fixedDeltaTime);    
    }
    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.instance.feetCollider.IsTouchingLayers(ground))
        {
            Player.instance.anim.Play("FallAttack2");
            FAD.transform.localScale = new Vector3(Player.instance.transform.localScale.x, FAD.transform.localScale.y, FAD.transform.localScale.z);
            Instantiate(FAD, new Vector3 (Player.instance.transform.position.x, Player.instance.transform.position.y-1, Player.instance.transform.position.z), Quaternion.identity);
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
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
