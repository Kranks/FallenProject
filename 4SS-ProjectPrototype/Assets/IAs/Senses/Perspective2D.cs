using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective2D : Sense {

    [Range(0, 360)]
    public float viewAngle;
    public float viewRadius;
    public Aspect.AspectTypes targetAspect;
    private Vector2 direction = Vector2.down;

    public List<Transform> visibleTargets = new List<Transform>();

    void DetectTargets() {
        visibleTargets.Clear();
        Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);
        RaycastHit2D hit;
        for (int i = 0; i < targetInViewRadius.Length; i++) {
            Transform target = targetInViewRadius[i].transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(direction, dirToTarget) < viewAngle / 2) {
                float dstToTarget = Vector2.Distance(transform.position, target.position);
                hit = Physics2D.Raycast(/*transform.position*/GetComponent<EnemyController2D>().spawnSkill.transform.position, dirToTarget, dstToTarget);              
                if (hit) {                    
                    Aspect aspect = hit.collider.GetComponent<Aspect>();
                    if (aspect != null) {
                        //Check the aspect
                        if (aspect.aspectType == targetAspect) {
                            print("Enemy Detected");
                            visibleTargets.Add(target);
                        }
                    }
                }
            }
        }
    }
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.z;
        }
        if (direction == Vector2.up) {
            return new Vector2(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        } else if (direction == Vector2.left) {
            return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
        } else if (direction == Vector2.right) {
            return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), -Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
        } else {
            // direction == Vector.down
            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), -Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        
    }

    protected override void Initialize() {
    }

    public bool IsTargetVisible() {
        return (visibleTargets.Count > 0) ? true : false;
    }

    public Transform target() {
        return (visibleTargets.Count > 0) ? visibleTargets[0] : null;

    }

    public void UpdateDirection(Vector2 newDirection) {
        direction = newDirection;
    }
    public override void UpdateSense() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= detectionRate) {
            
            DetectTargets();
        }
    }
}
