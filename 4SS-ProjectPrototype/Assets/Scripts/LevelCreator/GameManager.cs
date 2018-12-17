using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 5;
    private int roomNumber = 2;
	// Use this for initialization
	void Awake () {
        if (instance==null)
        {
            instance = this;
        } else if (instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}
	
    void InitGame()
    {
        boardScript.GridClear();
        Room room1 = new Room(new Vector3(0,0,0),boardScript,level,0,3,10);
        //Room room2 = new Room(new Vector3((room1.getColumns()/2)+1, room1.getRows()+3, 0), boardScript, level);
        //Room room3 = new Room(new Vector3((room2.getOffset().x)-(room2.getColumns()+2), room2.getOffset().y + 3, 0), boardScript, level);
        //room3.setOffset(new Vector3((room2.getOffset().x) - (room3.getColumns() + 4), room2.getOffset().y, 0));*/
        //boardScript.SetupScene(level);
        room1.DrawRoom();
        //room2.DrawRoom();
        //room3.DrawRoom();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
