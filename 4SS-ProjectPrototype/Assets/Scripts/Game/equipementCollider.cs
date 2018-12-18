using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipementCollider : MonoBehaviour {

    public PlayerController player;
    public GameObject MenuEquiper;
    public GameObject MenuPreview;
    public GameObject MenuPreviewEnCours;
    public GameObject prefabEquipement;

    [SerializeField]
    public Equipement equip;


    [Header("MenuPreview")]
    [SerializeField]
    private Image itemPreview;
    [SerializeField]
    private Text namePreview;
    [SerializeField]
    private Text lifePreview;
    [SerializeField]
    private Text attaquePreview;
    [SerializeField]
    private Text competence;
    [SerializeField]
    private Text defensePreview;
    [SerializeField]
    private Text precisionPreview;
    [SerializeField]
    private Text speedPreview;

    [Header("MenuPreviewEncours")]
    [SerializeField]
    private Image itemPreviewEncours;
    [SerializeField]
    private Text namePreviewEncours;
    [SerializeField]
    private Text lifePreviewEncours;
    [SerializeField]
    private Text attaquePreviewEncours;
    [SerializeField]
    private Text competenceEncours;
    [SerializeField]
    private Text defensePreviewEncours;
    [SerializeField]
    private Text precisionPreviewEncours;
    [SerializeField]
    private Text speedPreviewEncours;



    private void Start()
    {
       
        //equipement = new Equipement("casque en bois", 1, 2, 2, 2, "casque", ScriptableObject.CreateInstance<Spell>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            MenuEquiper.SetActive(true);
            MenuPreview.SetActive(true);
            MenuPreviewEnCours.SetActive(true);

            foreach(Equipement equi in player.player.equipements)
            {
                if(equip.tag == equi.tag)
                {
                    itemPreviewEncours.sprite = equi.spriteUI;
                    namePreviewEncours.text = equi.name;
                    lifePreviewEncours.text = equi.life.ToString();
                    attaquePreviewEncours.text = equi.attack.ToString();
                    defensePreviewEncours.text = equi.defence.ToString();
                    speedPreviewEncours.text = equi.speed.ToString();
                    competenceEncours.text = equi.competence.description;
                }

                itemPreview.sprite = equip.spriteUI;
                namePreview.text = equip.name;
                lifePreview.text = equip.life.ToString();
                attaquePreview.text = equip.attack.ToString();
                defensePreview.text = equip.defence.ToString();
                speedPreview.text = equip.speed.ToString();
                competence.text = equip.competence.description;

            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MenuEquiper.SetActive(false);
        MenuPreview.SetActive(false);
        MenuPreviewEnCours.SetActive(false);
    }

    public void fermer()
    {
        MenuEquiper.SetActive(false);
        MenuPreview.SetActive(false);
        MenuPreviewEnCours.SetActive(false);
        prefabEquipement.SetActive(false);

    }

    public void equiperOnClik()
    {
        player.equiperItem(equip);
    }
    //public void equiperItem()
    //{

    //    for (int i = 0; i < this.player.equipements.Count; i++)
    //    {
    //        if (this.equipement.tag == this.player.equipements[i].tag)
    //        {
    //            this.player.equipements[i] = this.equipement;
    //            Debug.Log(this.equipement.nom);
    //        }
    //    }

    //    MenuEquiper.SetActive(false);
    //    MenuPreview.SetActive(false);
    //    player.stat(this.player);
    //}
}
