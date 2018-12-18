using Assets.Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEquipement {

	public static ScriptableObject createEquipement(string tag)        
    {
        FactorySpell factory = new FactorySpell();
        System.Random rand = new System.Random();
        int alea = rand.Next(1,3);
        
        switch (tag)
        {
            case "casque":
                if(alea == 1)
                {
                    Equipement casqueEnBois = ScriptableObject.CreateInstance<Equipement>();
                    casqueEnBois.attack = 2;
                    casqueEnBois.competence = (Spell)factory.createSpell();
                    casqueEnBois.criticalDamage = 0;
                    casqueEnBois.defence = 5;
                    casqueEnBois.criticalRate = 0;
                    casqueEnBois.nom = "Casque en bois";
                    casqueEnBois.life = 10;
                    
                    casqueEnBois.spriteUI = Resources.Load<Sprite>("UI Equipement/hlm_t_10");
                    Debug.Log(casqueEnBois.spriteUI);
                    casqueEnBois.tag = "casque";
                    casqueEnBois.speed = 0;
                    return casqueEnBois;
                }
                else if (alea == 2)
                {
                    Equipement casqueEnBois = ScriptableObject.CreateInstance<Equipement>();
                    casqueEnBois.attack = 2;
                    casqueEnBois.competence = (Spell)factory.createSpell();
                    casqueEnBois.criticalDamage = 0;
                    casqueEnBois.defence = 5;
                    casqueEnBois.criticalRate = 0;
                    casqueEnBois.nom = "Casque en bois";
                    casqueEnBois.life = 10;
                    casqueEnBois.spriteUI = Resources.Load<Sprite>("UI Equipement/hlm_t_10");
                    Debug.Log(casqueEnBois.spriteUI);
                    casqueEnBois.tag = "casque";
                    casqueEnBois.speed = 0;
                    return casqueEnBois;
                }
                else
                {
                    Equipement casqueEnBois = ScriptableObject.CreateInstance<Equipement>();
                    casqueEnBois.attack = 2;
                    casqueEnBois.competence = (Spell)factory.createSpell();
                    casqueEnBois.criticalDamage = 0;
                    casqueEnBois.defence = 5;
                    casqueEnBois.criticalRate = 0;
                    casqueEnBois.nom = "Casque en bois";
                    casqueEnBois.life = 10;
                    casqueEnBois.spriteUI = Resources.Load<Sprite>("UI Equipement/hlm_t_10");
                    Debug.Log(casqueEnBois.spriteUI);
                    casqueEnBois.tag = "casque";
                    casqueEnBois.speed = 0;
                    return casqueEnBois;
                }

            default:
                throw new Exception("Cet objet n'existe pas");
        }
    }
}
