using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/ProjectileSpell", fileName = "newSpell")]
public class ProjectileSpell : Spell {

    public float projectileSpeed;

    public override void launch(Vector3 position) {
        GameObject proj = Instantiate(prefab, position, Quaternion.identity);
        proj.GetComponent<Projectile>().Initialize(this);
    }
    public override void launch(Vector3 position, Transform target)
    {
        GameObject proj = Instantiate(prefab, position, Quaternion.identity);
        Vector2 direction = (target.position- position).normalized;
        proj.GetComponent<Projectile>().Initialize(this, direction);
    }

}
