using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Characters/Enemy", fileName = "newEnemy")]
public class EnemyInfo : ScriptableObject {

    public string enemyName;

    public float life;
    public float speed;

    public float precisionCoef;
    public float dodgeCoef;

    public bool shortRange; 

    public Sprite persoUp;
    public Sprite persoDown;
    public Sprite persoLeft;
    public Sprite persoRight;

    public List<Spell> spells;

    public void SetRange(EnemyController2D enemy) {
        enemy.range = 0f;
        enemy.maxRange = 0f;
        foreach (var spell in enemy.infos.spells) {
            if (!shortRange) {
                if (enemy.range < spell.portee)
                    enemy.range = spell.portee * 0.8f;
            } else {
                if (enemy.range == 0)
                    enemy.range = (spell.portee == 0f) ? spell.portee : spell.portee * 0.9f;

                if (enemy.range > spell.portee)
                    enemy.range = spell.portee * 0.9f;
            }

            if (enemy.maxRange < spell.portee) {
                enemy.maxRange = spell.portee;
            }
        }
    }

}
