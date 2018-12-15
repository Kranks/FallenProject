using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipementCollider : MonoBehaviour {

    public PlayerInfo player;
    public Equipement equipement;

    public GameObject MenuEquiper;
    public GameObject MenuPreview;

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

    private void Start()
    {
        //equipement = new Equipement("casque en bois", 1, 2, 2, 2, "casque", ScriptableObject.CreateInstance<Spell>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>().player;

        MenuEquiper.SetActive(true);
        MenuPreview.SetActive(true);

        attaquePreview.text = equipement.attack.ToString();
        defensePreview.text = equipement.defence.ToString();
        speedPreview.text = equipement.speed.ToString();
        competence.text = equipement.competence.description;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MenuEquiper.SetActive(false);
        MenuPreview.SetActive(false);
    }

    public void fermer()
    {
        equipement = null;
        MenuEquiper.SetActive(false);
        MenuPreview.SetActive(false);
    }
    public void equiperItem()
    {

        for (int i = 0; i < this.player.equipements.Count; i++)
        {
            if (this.equipement.tag == this.player.equipements[i].tag)
            {
                this.player.equipements[i] = this.equipement;
                Debug.Log(this.equipement.nom);
            }
        }

        MenuEquiper.SetActive(false);
        MenuPreview.SetActive(false);
        player.stat(this.player);
    }
}
