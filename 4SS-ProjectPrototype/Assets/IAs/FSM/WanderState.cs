using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : StateMachineBehaviour {

    private Vector3 targetPosition;
    public float movementSpeed = 0.65f;
    private float targetPositionTolerance = 0.5f;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void GetNextPosition() {
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GetNextPosition();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Vector3.Distance(targetPosition, animator.transform.position) <= targetPositionTolerance) {
            GetNextPosition();
        }
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

}
