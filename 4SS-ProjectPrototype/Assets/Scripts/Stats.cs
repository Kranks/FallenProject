using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Characters/Stat", fileName = "newStat")]
public class Stats : ScriptableObject {

    public float life;
    public float attack;
    public float defence;
    public float speed;
    public float criticalRate;
    public float criticalDamage;
    public float precisionCoef;
    public float dodgeCoef;

    public void Add(Stats stats) {
        life  += stats.life;
        attack += stats.attack;
        defence += stats.defence;
        speed += stats.speed;
        criticalRate += stats.criticalRate;
        criticalDamage += stats.criticalDamage;
        precisionCoef += stats.precisionCoef;
        dodgeCoef += stats.dodgeCoef;
    }

    public void Remove(Stats stats) {
        life -= stats.life;
        attack -= stats.attack;
        defence -= stats.defence;
        speed -= stats.speed;
        criticalRate -= stats.criticalRate;
        criticalDamage -= stats.criticalDamage;
        precisionCoef -= stats.precisionCoef;
        dodgeCoef -= stats.dodgeCoef;
    }

}
