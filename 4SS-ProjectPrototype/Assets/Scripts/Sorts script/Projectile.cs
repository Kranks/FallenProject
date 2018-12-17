using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage;
    public float projectileSpeed;

    public Animator animator;

    public void Initialize(ProjectileSpell s) {
        damage = s.degats;
        projectileSpeed = s.projectileSpeed;
 
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(directionSort() * projectileSpeed);
     
    }

    public void Initialize(ProjectileSpell s, Vector2 direction)
    {
        damage = s.degats;
        projectileSpeed = s.projectileSpeed;

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);

    }
    private Vector2 directionSort() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        return direction.normalized;
    }

    public void ResolveDamage(EnemyController2D enemy) {
        float resultDamage = damage;
        if (Random.Range(0f, 100f) > enemy.infos.stats.dodgeCoef) {
            enemy.currentlife -= resultDamage;            
        } else {
            // set feedback
        }
    }

    public void ResolveDamage(PlayerController player) {
        float resultDamage = damage - (player.player.stats.defence * 0.3f);
        player.life -= resultDamage;
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy") {
            ResolveDamage(collision.gameObject.GetComponent<EnemyController2D>());
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player") {
            ResolveDamage(collision.gameObject.GetComponent<PlayerController>());
            Destroy(this.gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            ResolveDamage(collision.gameObject.GetComponent<EnemyController2D>());
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            ResolveDamage(collision.gameObject.GetComponent<PlayerController>());
            Destroy(this.gameObject);
        }
    }
}
