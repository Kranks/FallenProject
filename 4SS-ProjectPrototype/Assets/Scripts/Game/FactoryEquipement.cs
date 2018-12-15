using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEquipement {

	public Equipement createEquipement(string tag)        
    {
        System.Random rand = new System.Random();
        int alea = rand.Next(1,3);
        
        switch (tag)
        {
            case "casque":
                //if(alea == 1)
                //{
                //    return new Equipement("casque en boi",1, 1, 1, 1, tag, ScriptableObject.CreateInstance<Spell>());
                //}
                //else if (alea == 2)
                //{
                //    return new Equipement("casque en fer",2, 1, 1, 1, tag, ScriptableObject.CreateInstance<Spell>());
                //}
                //else
                //{
                //    return new Equipement("casque en acier",3, 1, 1, 1, tag, ScriptableObject.CreateInstance<Spell>());
                //}                

            default:
                throw new Exception("Cet objet n'existe pas");
        }
    }
}
