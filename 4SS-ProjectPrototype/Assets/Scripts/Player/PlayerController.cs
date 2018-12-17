using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image vie;
    
    public PlayerInfo player;
    public float life;

    private Rigidbody2D body;
    private SpriteRenderer image;
    [SerializeField]
    private Sprite persoUp;
    [SerializeField]
    private Sprite persoDown;
    [SerializeField]
    private Sprite persoLeft;
    [SerializeField]
    private Sprite persoRight;
    private Animator animator;
    private Vector2 direction;

    private GameObject spawSkill;
    public GameObject spawSkillRight;
    public GameObject spawSkillLeft;
    public GameObject spawSkillUp;
    public GameObject spawSkillDown;

    public GameObject MenuEquiper;
    public GameObject MenuPreview;

    public Dictionary<string, int> dicoTagEquip;

    public bool end = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        life = player.stats.life;

        dicoTagEquip = player.GenerateDico();
    }
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        body.MovePosition(body.position + new Vector2(moveHorizontal, moveVertical) * player.stats.speed * Time.deltaTime);
        
       // GetInputSort();
        getInputDeplacement();
        SpriteUpdate();
        animate();
        vie.fillAmount = (float)life / (float)100.0;
        if (life <= 0 && !end)
        {
            SceneManager.LoadScene("EndScene");
            end = true;

        }
    }

    public void getInputDeplacement()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            image.sprite = persoUp;
            direction = Vector2.up.normalized;
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            image.sprite = persoDown;
            direction = Vector2.down.normalized;
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            image.sprite = persoLeft;
            direction = Vector2.left.normalized;
            Debug.Log("Input");
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            image.sprite = persoRight;
            direction = Vector2.right.normalized;
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }
        if (!Input.anyKey)
        {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(0, 1);
        }

    }
    public void GetInputSort()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {

        }
        
    }

    public void SpriteUpdate()
    {

        Vector2 dirToTarget = directionSort();
        if (Vector3.Angle(Vector2.up, dirToTarget) < 30f)
        {
            direction = Vector2.up;
            spawSkill = spawSkillUp;
        }
        else if (Vector2.Angle(Vector2.down, dirToTarget) < 30f)
        {
            direction = Vector2.down;
            spawSkill = spawSkillDown;
        }
        else if (Vector2.Angle(Vector2.right, dirToTarget) < 30f)
        { 
            direction = Vector2.right;
            spawSkill = spawSkillRight;
        }
        else if (Vector2.Angle(Vector2.left, dirToTarget) < 30f)
        {
            direction = Vector2.left;
            spawSkill = spawSkillLeft;
        }
    }


    public void attack(int id) {
        SpriteUpdate();
        this.player.equipements[id].competence.launch(spawSkill.transform.position);
    }

    private void animate()
    {
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
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

    

}
