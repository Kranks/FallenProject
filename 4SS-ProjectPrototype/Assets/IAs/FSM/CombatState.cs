using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : StateMachineBehaviour {

    public EnemyController2D enemy;
    private Dictionary<Spell, float> spellTimer = new Dictionary<Spell, float>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemy = animator.GetComponent<EnemyController2D>();
        foreach (var spell in enemy.infos.spells) {
            spellTimer.Add(spell, 0);
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        float disToTarget = Vector3.Distance(enemy.currentTarget.position, animator.transform.position);

        foreach (var spell in enemy.infos.spells) {
            spellTimer[spell] -= Time.deltaTime;            
            if (spellTimer[spell] <= 0f && disToTarget <= spell.portee) {
                //Instantiate(spell.prefab, enemy.GetComponentInChildren<Transform>().position, Quaternion.identity);
                spell.launch(enemy.spawnSkill.transform.position, enemy.currentTarget, Vector3.zero);
                spellTimer[spell] = spell.cooldown;
            }
        }

        if ( disToTarget > enemy.maxRange) {
            animator.SetBool("atRange", false);
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        spellTimer.Clear();
	}

}
