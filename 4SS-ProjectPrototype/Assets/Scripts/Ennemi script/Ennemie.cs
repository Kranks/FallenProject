using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemie : MonoBehaviour {

    public int life = 100;
    public Image vie;
    public int lifeInprogress;

	// Use this for initialization
	void Start () {
        lifeInprogress = life;
	}
	
	// Update is called once per frame
	void Update () {

       
        if(this.life <= 0)
        {
            this.gameObject.SetActive(false);
        }
        //vie.fillAmount = (float)life / (float)lifeInprogress;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
       
    }



    public int Life { get { return life; } set { life = value; } }
}
