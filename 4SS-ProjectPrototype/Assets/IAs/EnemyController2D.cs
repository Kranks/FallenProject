using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController2D : MonoBehaviour {

    public EnemyInfo infos;
    public GameObject equipement;
    public Transform currentTarget;

    private Perspective2D view;

    private Rigidbody2D body;

    private SpriteRenderer image;

    public List<Transform> waypoints = new List<Transform>();
    

    private Animator states;
    public Image vie;

    public float currentlife;
    
    public GameObject spawnSkillLeft;
    public GameObject spawnSkillRight;
    public GameObject spawnSkillUp;
    public GameObject spawnSkillDown;
    [HideInInspector]
    public GameObject spawnSkill;

    public float range;
    public float maxRange;
    public float timeout = 4.0f;
    public float elapsedTime = 0.0f;
    private float maxLife;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        view = GetComponent<Perspective2D>();
        states = GetComponent<Animator>();
        view.UpdateDirection(Vector2.down);

        image.sprite = infos.persoDown;

        currentlife = infos.stats.life;
        infos.SetRange(this);
        spawnSkill = spawnSkillDown;
        maxLife = currentlife;

        equipement = GameObject.Find("EquipementPrefab");
        
    }

    void Update() {
        
        if (currentlife <= 0) {
            this.gameObject.SetActive(false);

            //Instantiate(EquipementPrefab, this.transform.position, Quaternion.identity);
            equipement.gameObject.SetActive(true);
            equipement.GetComponent<equipementCollider>().equip = (Equipement)FactoryEquipement.createEquipement("casque");
            equipement.transform.position = this.transform.position;
        }
        vie.fillAmount = (float)currentlife / maxLife;

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
                spawnSkill = spawnSkillUp;
                view.UpdateDirection(Vector2.up);
            } else if (Vector2.Angle(Vector2.down, dirToTarget) < 45f) {
                image.sprite = infos.persoDown;
                spawnSkill = spawnSkillDown;
                view.UpdateDirection(Vector2.down);
            } else if (Vector2.Angle(Vector2.right, dirToTarget) < 45f) {
                image.sprite = infos.persoRight;
                spawnSkill = spawnSkillRight;
                view.UpdateDirection(Vector2.right);
            } else if (Vector2.Angle(Vector2.left, dirToTarget) < 45f) {
                image.sprite = infos.persoLeft;
                spawnSkill = spawnSkillLeft;
                view.UpdateDirection(Vector2.left);
            }
        }
    }
    private void LateUpdate() {
        SpriteUpdate(currentTarget);
    }

}
