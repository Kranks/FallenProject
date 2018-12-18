using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject {

    public string nomSort;
    public float degats;
    public float portee;
    public float duree;
    public float cooldown;
    public string element;
    public Sprite sprite;
    public string description;
    public GameObject prefab;

    public abstract void launch(Vector3 position, Transform target);
    public abstract void launch(Vector3 position);

}
