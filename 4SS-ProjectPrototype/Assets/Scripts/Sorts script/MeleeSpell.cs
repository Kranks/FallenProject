using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Spell/MeleeSpell", fileName = "newSpell")]
public class MeleeSpell : Spell {

    public enum Zone {
        circle,
        line,
        column
    }

    public float width;
    public Zone zoneType;
    public Aspect.AspectTypes targetType;

    public override void launch(Vector3 position) {
        throw new System.NotImplementedException();
    }

    public override void launch(Vector3 position, Transform target, Vector3 dir) {
        if (target) {
            Vector2 direction = (target.position - position).normalized;
            Collider2D[] targets = GetColliders(zoneType, position, direction);
            foreach (var t in targets) {
                if (!t.isTrigger) {
                    Aspect aspect = target.GetComponent<Aspect>();
                    if (aspect) {
                        if (aspect.aspectType == targetType) {
                            if (target.GetComponent<EnemyController2D>()) {
                                ResolveDamage(target.GetComponent<EnemyController2D>());
                            }
                            if (target.GetComponent<PlayerController>()) {
                                ResolveDamage(target.GetComponent<PlayerController>());
                            }
                        }
                    }
                }
            }
        } else {
            Vector2 direction = dir;
            Collider2D[] targets = GetColliders(zoneType, position, direction);
            foreach (var t in targets) {
                if (!t.isTrigger) {
                    Aspect aspect = t.GetComponent<Aspect>();
                    if (aspect) {
                        if (aspect.aspectType == targetType) {
                            if (t.GetComponent<EnemyController2D>()) {
                                ResolveDamage(t.GetComponent<EnemyController2D>());
                            }
                            if (t.GetComponent<PlayerController>()) {
                                ResolveDamage(t.GetComponent<PlayerController>());
                            }
                        }
                    }
                }
            }
        }
    }

    public Collider2D[] GetColliders(Zone zone, Vector2 position, Vector2 direction) {
        Vector2 posA = Vector2.zero;
        Vector2 posB = Vector2.zero;
        switch (zone) {
            case Zone.circle:
                return Physics2D.OverlapCircleAll(position, portee);
            case Zone.line:
                posA = position + Vector2.Perpendicular(GridDirection(direction)) * (portee / 2);
                posB = position + width * direction + Vector2.Perpendicular(GridDirection(direction)) * (-portee/ 2);
                return Physics2D.OverlapAreaAll(posA, posB);
            case Zone.column:
                posA = position + Vector2.Perpendicular(GridDirection(direction)) * (width / 2);
                posB = position + portee * direction + Vector2.Perpendicular(GridDirection(direction))* portee * (-width / 2);
                return Physics2D.OverlapAreaAll(posA, posB);
            default:
                return Physics2D.OverlapCircleAll(position, portee);
        }
    }
    public Vector2 GridDirection(Vector2 vec) {
        Vector2 dir = Vector2.zero;
        if (Vector2.Angle(Vector2.up, vec) < 45f) {
            dir = Vector2.up;
        } else if (Vector2.Angle(Vector2.down, vec) < 45f) {
            dir = Vector2.down;
        } else if (Vector2.Angle(Vector2.right, vec) < 45f) {
            dir = Vector2.right;
        } else if (Vector2.Angle(Vector2.left, vec) < 45f) {
            dir = Vector2.left;
        }
        return dir;
    }

    public void ResolveDamage(EnemyController2D enemy) {
        float resultDamage = degats;
        Debug.Log(degats);
        if (Random.Range(0f, 100f) > enemy.infos.stats.dodgeCoef) {
            enemy.currentlife -= resultDamage;
        } else {
            // set feedback
        }
    }

    public void ResolveDamage(PlayerController player) {
        float resultDamage = degats - (player.player.stats.defence * 0.3f);
        player.life -= resultDamage;
    }
}
