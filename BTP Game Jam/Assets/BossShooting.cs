using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : StateMachineBehaviour
{
    public float Speed;

    Boss boss;
    GameObject Player;
    Transform currentpos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            return;
        }

        boss = animator.GetComponent<Boss>();
        currentpos = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player != null)
        {
            if (Vector2.Distance(currentpos.position, Player.transform.position) >= 10f)
            {
                currentpos.position = Vector2.MoveTowards(currentpos.position, Player.transform.position, Speed * Time.fixedDeltaTime);
            }

            else
            {
                currentpos.position = Vector2.MoveTowards(currentpos.position, Player.transform.position, -Speed * Time.fixedDeltaTime);
            }
        }

        boss.Shoot();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
