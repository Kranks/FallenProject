using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class FactorySpell
    {
        public ScriptableObject createSpell()
        {
            System.Random rand = new System.Random();
            int alea = 1;

            switch (alea)
            {
                case 1:
                    ProjectileSpell feu = ScriptableObject.CreateInstance<ProjectileSpell>();
                    feu.degats = 15;
                    feu.description = "Lance une boule de feu";
                    feu.cooldown = 5;
                    feu.projectileSpeed = 400;
                    feu.duree = 0;
                    feu.element = "feu";
                    feu.nomSort = "boule de feu";
                    feu.sprite = Resources.Load<Sprite>("UIskills/red (14)");
                    feu.prefab = Resources.Load<GameObject>("PrefabSpell/BouleDeFeu");
                    return feu;

                default:
                    throw new Exception("Cet objet n'existe pas");
            }
        }
    }
}
