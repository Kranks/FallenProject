using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Characters/Player", fileName = "newPlayer")]
public class PlayerInfo : ScriptableObject {
    
    public float life;
    public float attack;
    public float defence;
    public float speed;
    public float criticalRate;
    public float criticalDamage;

    public List<Equipement> equipements = new List<Equipement>();

    public void stat(PlayerInfo player)
    {
        life = 0f;
        attack = 0f;
        defence = 0f;
        speed = 0f;
        criticalRate = 0f;
        criticalDamage = 0f;

        foreach (Equipement equip in player.equipements)
        {
            life += equip.life;
            attack += equip.attack;
            defence += equip.defence;
            speed += equip.speed;
            criticalRate += equip.criticalRate; 
            criticalDamage += equip.criticalDamage;
        }
    }
    
    public void InitialzeFrom(PlayerInfo p) {
        life = p.life;
        attack = p.attack;
        defence = p.defence;
        speed = p.speed;
        criticalRate = p.criticalRate;
        criticalDamage = p.criticalDamage;
        equipements = p.equipements;
    }

    public Dictionary<string, int> GenerateDico() {
        Dictionary<string, int> dico = new Dictionary<string, int>();
        int id = 0;
        foreach (var item in equipements) {
            dico.Add(item.tag, id++);
        }

        return dico;
    }
}
