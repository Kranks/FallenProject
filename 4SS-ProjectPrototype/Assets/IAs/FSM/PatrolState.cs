using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PatrolState : StateMachineBehaviour {
    
    public List<Transform> waypoint;
    public EnemyController2D enemy;
    public Transform target;

    public float dstTolerance = 2.0f;
    private int idTarget = 0;
    public float waitTime = 1.0f;
    private float elpasedTime = 0.0f;
    private bool changeTarget = false;

    private void GetNextPosition() {
        if (idTarget >= waypoint.Count) {
            idTarget = 0;
        }

        if (waypoint.Count > 0) {
            target = waypoint[idTarget++].transform;
        } else {
            target = null;
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemy = animator.GetComponent<EnemyController2D>();
        enemy.currentTarget = null;
        waypoint = enemy.Waypoints();
        GetNextPosition();
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (changeTarget) {
            elpasedTime += Time.deltaTime;
            if (elpasedTime >= waitTime) {
                enemy.SpriteUpdate(target);
                elpasedTime = 0;
                changeTarget = false;
            }
        } else {
            if (target) {
                if (Vector3.Distance(target.position, animator.transform.position) <= dstTolerance) {
                    GetNextPosition();
                    changeTarget = true;
                } else {
                    animator.transform.position = Vector3.MoveTowards(animator.transform.position, target.position, enemy.infos.stats.speed * Time.deltaTime);
                }
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
