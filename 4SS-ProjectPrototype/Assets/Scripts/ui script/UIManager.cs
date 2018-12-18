using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // Use this for initialization

    public GameObject MenuEquipement;
    public GameObject MenuSynergie;
    public GameObject MenuParametre;

    private bool menuOuvert = false;

    [SerializeField]
    private Image skill1;
    [SerializeField]
    private Image skill2;
    [SerializeField]
    private Image skill3;
    [SerializeField]
    private Image skill4;
    [SerializeField]
    private Image skill5;
    [SerializeField]
    private Image skill6;
    [SerializeField]
    private Image skill7;
    [SerializeField]

    private PlayerController player;

    [SerializeField]
    private Image casque;
    [SerializeField]
    private Image torse;
    [SerializeField]
    private Image poignetDroit;
    [SerializeField]
    private Image poignetGauche;
    [SerializeField]
    private Image jambes;
    [SerializeField]
    private Image arme;
    [SerializeField]
    private Image bijou;
    [SerializeField]
    private Image pieds;

    [SerializeField]
    private Text vie;
    [SerializeField]
    private Text attaque;
    [SerializeField]
    private Text defense;
    [SerializeField]
    private Text attaqueCritique;
    [SerializeField]
    private Text domageCritique;
    [SerializeField]
    private Text speed;

    private List<Image> skills = new List<Image>();


    Dictionary<string, Image> dicoTagUI; 

    void Start()
    {
        skills.Add(skill1);
        skills.Add(skill2);
        skills.Add(skill3);
        skills.Add(skill4);
        skills.Add(skill5);
        skills.Add(skill6);
        skills.Add(skill7);

        dicoTagUI = new Dictionary<string, Image>();
        foreach (var item in skills) {
            dicoTagUI.Add(item.gameObject.GetComponent<spellCoolDown>().equipTag, item);
        }

        fillEquipement();
        fillStat();

    }

    // Update is called once per frame
    void Update()
    {
        fillEquipement();
        fillStat();
        fillSkillBar();
        mappageTouche();
    }

    public void ouvrirMenuParametre()
    {
        if (!menuOuvert)
        {
            MenuParametre.gameObject.SetActive(true);
            menuOuvert = true;
        }
    }

    public void fermerMenuParametre()
    {
        MenuParametre.gameObject.SetActive(false);
        menuOuvert = false;
    }

    public void ouvrirMenuEquipement()
    {
        if (!menuOuvert)
        {
            MenuEquipement.gameObject.SetActive(true);
            menuOuvert = true;
        }
    }

    public void fermerMenuEquipement()
    {
        MenuEquipement.gameObject.SetActive(false);
        menuOuvert = false;
    }

    public void ouvrirMenuSynergie()
    {
        if (!menuOuvert)
        {
            MenuSynergie.gameObject.SetActive(true);
            menuOuvert = true;
        }
    }

    public void fermerMenuSynergie()
    {
        MenuSynergie.gameObject.SetActive(false);
        menuOuvert = false;
    }

    public void fillSkillBar()
    {
        foreach (var item in dicoTagUI) {
            item.Value.sprite = player.player.equipements[player.dicoTagEquip[item.Key]].competence.sprite;
        }
    }

    public void fillEquipement()
    {
        foreach (Equipement equip in player.player.equipements)
        {
            switch (equip.tag)
            {
                case "casque":
                    casque.sprite = equip.spriteUI;
                    break;
                case "torse":
                    torse.sprite = equip.spriteUI;
                    break;
                case "arme":
                    arme.sprite = equip.spriteUI;
                    break;
                case "bijou":
                    bijou.sprite = equip.spriteUI;
                    break;
                case "jambes":
                    jambes.sprite = equip.spriteUI;
                    break;
                case "pieds":
                    pieds.sprite = equip.spriteUI;
                    break;
                case "gant":
                    poignetDroit.sprite = equip.spriteUI;
                    poignetGauche.sprite = ((Gant)equip).poignetG;
                    break;                
            }            
        }
    }

    public void fillStat()
    {
        float life = player.player.stats.life;
        float attack = player.player.stats.attack;
        float defence= player.player.stats.defence;
        float criticalRate = player.player.stats.criticalRate;
        float criticalDamage = player.player.stats.criticalDamage;
        float Vit=player.player.stats.speed;

        foreach (Equipement equip in player.player.equipements)
        {
            life += equip.life;
            attack += equip.attack;
            defence += equip.defence;
            criticalRate += equip.criticalRate;
            criticalDamage += equip.criticalDamage;
            Vit += equip.speed;
        }

        vie.text = life.ToString();
        attaque.text = attack.ToString();
        defense.text = defence.ToString();
        attaqueCritique.text = criticalRate.ToString();
        domageCritique.text = criticalDamage.ToString();
        speed.text = Vit.ToString();
    }

    public void ouvrirMap()
    {

    }

    public void mappageTouche()
    {
        if (Input.GetKey(KeyCode.I))
        {
            ouvrirMenuEquipement();             
        }
        if (Input.GetKey(KeyCode.M))
        {            
            ouvrirMap();
        }
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
