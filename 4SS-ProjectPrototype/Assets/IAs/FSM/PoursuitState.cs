using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoursuitState : StateMachineBehaviour {

    public EnemyController2D enemy;
    private float timeout = 5.0f;
    private float elapsedTime = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemy = animator.GetComponent<EnemyController2D>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    if (enemy.currentTarget) {
            if (Vector3.Distance(enemy.currentTarget.position, animator.transform.position) <= enemy.range) {
                // AttaqueState
                animator.SetBool("atRange", true);
            }
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, enemy.currentTarget.position, enemy.infos.stats.speed * Time.deltaTime);
            
        } else {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeout) {
                animator.SetBool("targetFound", false);
                elapsedTime = 0.0f;
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}
}
