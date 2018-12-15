using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellCoolDown : MonoBehaviour {

    public Image darkMask;
    public Text coolDownTextDisplay;

    public string equipTag;
    private float coolDownDuration;
    private float coolDownTimeLeft;
    private float nextReadyTime;
    public Button button;
    public PlayerController player;
    public bool start = true;
    
    
    public void initialize()
    {
        Spell spell = player.player.equipements[player.dicoTagEquip[equipTag]].competence;
        darkMask.sprite = spell.sprite;
        coolDownDuration = spell.cooldown;
        spellReady();
    }

	// Update is called once per frame
	void Update () {

        if (start) {
            initialize();
            start = false;
        }

        bool coolDownCompleted = (Time.time > nextReadyTime);
        if (coolDownCompleted)
        {
            spellReady();
            if (Input.GetKey(KeyCode.Alpha1) && equipTag == "arme")
            {
                clicked();
                player.attack(player.dicoTagEquip["arme"]);
            }
            else if(Input.GetKey(KeyCode.Alpha2) && equipTag == "casque")
            {
                clicked();
                player.attack(player.dicoTagEquip["casque"]);
            }
            else if (Input.GetKey(KeyCode.Alpha3) && equipTag == "torse")
            {
                clicked();
                player.attack(player.dicoTagEquip["torse"]);
            }
            else if (Input.GetKey(KeyCode.Alpha4) && equipTag == "gant")
            {
                clicked();
                player.attack(player.dicoTagEquip["gant"]);
            }
            else if (Input.GetKey(KeyCode.Alpha5) && equipTag == "jambes")
            {
                clicked();
                player.attack(player.dicoTagEquip["jambes"]);
            }
            else if (Input.GetKey(KeyCode.Alpha6) && equipTag == "pieds")
            {
                clicked();
                player.attack(player.dicoTagEquip["pieds"]);
            }
            else if (Input.GetKey(KeyCode.Alpha7) && equipTag == "bijou" )
            {
                clicked();
                player.attack(player.dicoTagEquip["bijou"]);
            }
        }
        else
        {
            CoolDown();
        }
	}

    private void spellReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
        button.enabled = true;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedcd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedcd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    public void clicked()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
        button.enabled = false;        
    }
}
