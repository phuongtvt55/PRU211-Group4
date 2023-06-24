using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_behaviour : StateMachineBehaviour
{
    private NecromanceController necromance;
    private Rigidbody2D rb;
    [SerializeField]
    private float movement;
    [SerializeField]
    private GameObject knight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        necromance = animator.GetComponent<NecromanceController>();
        rb = necromance.GetComponent<Rigidbody2D>();
        necromance.FollowPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        necromance.FollowPlayer();
        bool isFacingRight = necromance.isFacingRight;
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
