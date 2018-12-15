using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveProjectile : MonoBehaviour {

    public Rigidbody2D projectile;
    public float moveSpeed = 5.0f;
    public GameObject ennemie;
    public Animator animator;
    public Spell sort;
    public GameObject player;
    

	// Use this for initialization
	void Start () {
        projectile = this.gameObject.GetComponent<Rigidbody2D>();
        projectile.AddForce(directionSort() * moveSpeed);
        
	}

    private Vector2 directionSort()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        return direction.normalized;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "background")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            //ennemie.GetComponent<Ennemie>().Life -= (int)this.GetComponent<DataSpell>().Degat;
            
            animator.SetBool("explosion", true);
            Destroy(this.gameObject);

        }
        if (collision.gameObject.tag == "Player")
        {/*
            player.GetComponent<Player>().Life -= (int)this.GetComponent<DataSpell>().Degat;

            animator.SetBool("explosion", true);
            Destroy(this.gameObject);
            */
        }
    }
}
