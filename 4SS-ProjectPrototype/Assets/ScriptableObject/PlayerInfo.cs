using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Characters/Player", fileName = "newPlayer")]
public class PlayerInfo : ScriptableObject {

    public Stats stats;
    
    public List<Equipement> equipements = new List<Equipement>();

    public void stat(PlayerInfo player)
    {
        stats.life = 0f;
        stats.attack = 0f;
        stats.defence = 0f;
        stats.speed = 0f;
        stats.criticalRate = 0f;
        stats.criticalDamage = 0f;

        foreach (Equipement equip in player.equipements)
        {
            stats.life += equip.life;
            stats.attack += equip.attack;
            stats.defence += equip.defence;
            stats.speed += equip.speed;
            stats.criticalRate += equip.criticalRate; 
            stats.criticalDamage += equip.criticalDamage;
        }
    }
    
    public void InitialzeFrom(PlayerInfo p) {
        stats.life = p.stats.life;
        stats.attack = p.stats.attack;
        stats.defence = p.stats.defence;
        stats.speed = p.stats.speed;
        stats.criticalRate = p.stats.criticalRate;
        stats.criticalDamage = p.stats.criticalDamage;
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
