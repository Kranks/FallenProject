using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour{

    public string nextScene;

    public void launchNextScene()
    {
            SceneManager.LoadScene(nextScene);
    }

 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            launchNextScene();
        }
    }
}
