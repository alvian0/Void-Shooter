using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : StateMachineBehaviour
{
    public float Speed = 20f;

    Transform Player;
    Transform currentpos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch
        {
            return;
        }

        currentpos = animator.GetComponent<Transform>();
        animator.GetComponent<Boss>().IsMeleeAttack = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
/*        if (Player != null)
        {
            if (Vector2.Distance(currentpos.position, Player.position) >= 4.5f)
            {
                currentpos.position = Vector2.MoveTowards(currentpos.position, Player.position, Speed * Time.fixedDeltaTime);
            }
        }
*/    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss>().IsMeleeAttack = false;
    }
}
