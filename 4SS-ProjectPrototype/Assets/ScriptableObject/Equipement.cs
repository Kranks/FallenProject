using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Equipement", fileName = "newEquipement")]
public class Equipement : ScriptableObject {

    public string nom;

    public float life;
    public float attack;
    public float defence;
    public float criticalRate;
    public float criticalDamage;
    public float speed;

    public Spell competence;

    public string tag;
    public Sprite spriteUI;
    //public GameObject prefab;
    
}

