using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spell_behaviour : StateMachineBehaviour
{
    [SerializeField]
    private GameObject spell;
    [SerializeField]
    private float offsetY;
    private NecromanceController necromanceController;
    private Transform player;
    DamageManage damageManage;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        necromanceController = animator.GetComponent<NecromanceController>();
        player = necromanceController.player;
        necromanceController.FollowPlayer();
        Vector2 positionSpell = new Vector2(player.position.x, player.position.y + offsetY);
        Vector2 positionSpell2 = new Vector2(player.position.x, player.position.y - offsetY);
        Instantiate(spell, positionSpell, Quaternion.identity);
        GameObject sp = Instantiate(spell, positionSpell2, Quaternion.identity);
        sp.transform.localScale *= new Vector2(-1, 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
