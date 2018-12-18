using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipementManager : MonoBehaviour {

    public GameObject equipement;
    bool desactivate = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!desactivate)
        {
            equipement.SetActive(false);
            desactivate = true;
        }
        
    }
}
