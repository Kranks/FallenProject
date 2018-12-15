using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour {

    public EnemyInfo infos;

    public Transform currentTarget;

    private Perspective2D view;

    private Rigidbody2D body;

    private SpriteRenderer image;

    public List<Transform> waypoints = new List<Transform>();

    private Animator states;

    public float currentlife;

    public float range;
    public float maxRange;
    public float timeout = 4.0f;
    public float elapsedTime = 0.0f;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        view = GetComponent<Perspective2D>();
        states = GetComponent<Animator>();
        view.UpdateDirection(Vector2.down);

        image.sprite = infos.persoDown;

        currentlife = infos.life;
        infos.SetRange(this);
    }

    void Update() {

        if (currentlife <= 0) {
            this.gameObject.SetActive(false);
            
        }
        
        view.UpdateSense();
        if (view.IsTargetVisible()) {
            currentTarget = view.target();
            states.SetBool("targetFound", true);
            elapsedTime = 0f;
        } else {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeout) {
                states.SetBool("targetFound", false);
                elapsedTime = 0.0f;
            }
        }

    }

    public List<Transform> Waypoints () {
        return waypoints;
    }

    public void SpriteUpdate(Transform target) {
        if (target) {
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(Vector2.up, dirToTarget) < 45f) {
                image.sprite = infos.persoUp;
                view.UpdateDirection(Vector2.up);
            } else if (Vector2.Angle(Vector2.down, dirToTarget) < 45f) {
                image.sprite = infos.persoDown;
                view.UpdateDirection(Vector2.down);
            } else if (Vector2.Angle(Vector2.right, dirToTarget) < 45f) {
                image.sprite = infos.persoRight;
                view.UpdateDirection(Vector2.right);
            } else if (Vector2.Angle(Vector2.left, dirToTarget) < 45f) {
                image.sprite = infos.persoLeft;
                view.UpdateDirection(Vector2.left);
            }
        }
    }
    private void LateUpdate() {
        SpriteUpdate(currentTarget);
    }

}
