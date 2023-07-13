using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerBehaviour : StateMachineBehaviour
{
    private BringerController bringer;
    private Rigidbody2D rb;
    [SerializeField]
    private float movement;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bringer = animator.GetComponent<BringerController>();
        rb = bringer.GetComponent<Rigidbody2D>();
        bringer.FollowPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bringer.FollowPlayer();
        bool isFacingRight = bringer.isFacingRight;
        if (isFacingRight)
        {
            rb.velocity = new Vector2(movement, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-movement, rb.velocity.y);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
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
