using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Spell/BuffSpell", fileName = "newSpell")]
public class BuffSpell : Spell {

    public Stats stats;
    public Aspect.AspectTypes targetType;

    public override void launch(Vector3 position) {
        Collider2D[] targets = Physics2D.OverlapCircleAll(position, portee);
        foreach (var target in targets) {
            Aspect aspect = target.GetComponent<Aspect>();
            if (aspect.aspectType == targetType) {
                if (target.GetComponent<EnemyController2D>())
                    Buff(target.GetComponent<EnemyController2D>());
                if (target.GetComponent<PlayerController>()) {
                    Buff(target.GetComponent<PlayerController>());
                }
            }
        }
    }

    public override void launch(Vector3 position, Transform target) {
        throw new System.NotImplementedException();
    }

    IEnumerator Buff(EnemyController2D enemy) {
        enemy.infos.stats.Add(stats);
        yield return new WaitForSeconds(duree);
        enemy.infos.stats.Remove(stats);
    }

    IEnumerator Buff(PlayerController player) {
        player.player.stats.Add(stats);
        yield return new WaitForSeconds(duree);
        player.player.stats.Remove(stats);
    }
}
