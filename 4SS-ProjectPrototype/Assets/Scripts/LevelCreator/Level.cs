using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public static Level instance = null;
    public BoardManager boardScript;
    public int minSizeRoom;
    public int maxSizeRoom;
    public int minNumberRoom;
    public int maxNumberRoom;

    private int levelNumber = 1;
    private int numberAttemptMaxCreationRoom = 3;
    private List<Room> roomList = new List<Room>();
    private List<Hallway> hallwayList = new List<Hallway>();


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void DrawAllRooms()
    {
        for(int room = 0; room < roomList.Count; room++)
        {
            roomList[room].DrawRoom();
        }
    }

    void DrawAllHallways()
    {
        for (int hallway = 0; hallway < hallwayList.Count; hallway++)
        {
            if(hallwayList[hallway].HallwayIsLinked())
            {
                boardScript.drawFullHallWay(hallwayList[hallway], hallwayList[hallway].getLinkedHallway());
            }
        }
    }

    void CreateRoomAndAddToList(Vector3 ofse, BoardManager bm,int ln,int pro)
    {
        Room newRoom = new Room(ofse,bm,ln, pro,minSizeRoom, maxSizeRoom);
        roomList.Add(newRoom);
    }

    bool CreateRoomAndAddToListFromRoom(Room departRoom, int numeroTentative)
    {
        bool newRoomCreated = false;

        /*On prend un hallway de la salle qi ne soit pas deja relie a un autre hallway d'une autre salle*/
        Hallway hallwaySelectedFromRoom = departRoom.selectRandomLinkableHallway();
        /*On creer une nouvelle salle avec un offset nul pour le moment*/
        /*La taille minimum de la salle est la taille de l'hallway pris +2 pour pouvoir dessiner l'hallway*/
        Room newRoom = new Room(Vector3.zero, boardScript, levelNumber, (departRoom.getProfondeur()+1), Mathf.Max((hallwaySelectedFromRoom.hallwaySize()+2),minSizeRoom), maxSizeRoom);
        Vector3 ofse = departRoom.getOffset();

        if(hallwaySelectedFromRoom.isReal())
        {
            /*On defini ici les longueurs min et max d'un hallway*/
            int hallwayLength = Random.Range(2, 10);
            /*On creer des vecteurs 3 qui correspondront au coordonnees du nouvel hallway au niveau local et mondial*/
            Vector3 firstLocalSquareNewHallway = hallwaySelectedFromRoom.getFirstLocalSquare();
            Vector3 lastLocalSquareNewHallway = hallwaySelectedFromRoom.getLastLocalSquare();
            Vector3 firstWorldSquareNewHallway = hallwaySelectedFromRoom.getFirstWorldSquare();
            Vector3 lastWorldSquareNewHallway = hallwaySelectedFromRoom.getLastWorldSquare();
            /*On creer des entiers qui correspondent a l'axe et a la direction du nouvel hallway*/
            int axeNewHallway = 0;
            int directionNewHallway = 0;
            /*axe=0 correspond a l'axe vertical*/
            if (hallwaySelectedFromRoom.getAxe() == 0)
            {
                /*direction=0 correspond a left*/
                if (hallwaySelectedFromRoom.getDirection() == 0)
                {
                    ofse.x = (departRoom.getOffset().x-2) - (hallwayLength+newRoom.getColumns());
                    firstLocalSquareNewHallway.x = newRoom.getColumns();
                    lastLocalSquareNewHallway.x = newRoom.getColumns();
                    firstWorldSquareNewHallway.x = firstWorldSquareNewHallway.x - hallwayLength-1;
                    lastWorldSquareNewHallway.x = lastWorldSquareNewHallway.x - hallwayLength-1;
                    axeNewHallway = 0;
                    directionNewHallway = 1;

                }
                /*direction=1 correspond a right*/
                else
                {
                    ofse.x = (departRoom.getOffset().x+2) + (hallwayLength + departRoom.getColumns());
                    firstLocalSquareNewHallway.x = -1;
                    lastLocalSquareNewHallway.x = -1;
                    firstWorldSquareNewHallway.x = firstWorldSquareNewHallway.x + hallwayLength+1;
                    lastWorldSquareNewHallway.x = lastWorldSquareNewHallway.x + hallwayLength+1;
                    axeNewHallway = 0;
                    directionNewHallway = 0;
                }
                /*Pour l'axe vertical on change l'offset en y*/

                ofse.y = (int)Random.Range(lastWorldSquareNewHallway.y-newRoom.getRows()+1,firstWorldSquareNewHallway.y);
            }
            /*axe=1 correspond a l'axe horizontal*/
            else
            {
                /*direction=0 correspond a down*/
                if (hallwaySelectedFromRoom.getDirection() == 0)
                {
                    ofse.y = (departRoom.getOffset().y-2) - (hallwayLength + newRoom.getRows());
                    firstLocalSquareNewHallway.y = newRoom.getRows();
                    lastLocalSquareNewHallway.y = newRoom.getRows();
                    firstWorldSquareNewHallway.y = firstWorldSquareNewHallway.y - hallwayLength-1;
                    lastWorldSquareNewHallway.y = lastWorldSquareNewHallway.y - hallwayLength-1;
                    axeNewHallway = 1;
                    directionNewHallway = 1;
                }
                /*direction=1 correspond a up*/
                else
                {
                    ofse.y = (departRoom.getOffset().y+2) + (hallwayLength + departRoom.getRows());
                    firstLocalSquareNewHallway.y = -1;
                    lastLocalSquareNewHallway.y = -1;
                    firstWorldSquareNewHallway.y = firstWorldSquareNewHallway.y + hallwayLength+1;
                    lastWorldSquareNewHallway.y = lastWorldSquareNewHallway.y + hallwayLength+1;
                    axeNewHallway = 1;
                    directionNewHallway = 0;
                }
                /*Pour l'axe horizontal on change l'offset en x*/

                ofse.x = (int)Random.Range(lastWorldSquareNewHallway.x-newRoom.getColumns(), firstWorldSquareNewHallway.x);

            }


            /*On instancie l'offset de la nouvelle salle*/
            newRoom.setOffset(ofse);
            /*On creer le nouvel hallway de la salle pour le relier a l'ancienne*/
            Hallway newHallway = new Hallway(firstWorldSquareNewHallway-ofse, lastWorldSquareNewHallway-ofse, firstWorldSquareNewHallway, lastWorldSquareNewHallway,axeNewHallway,directionNewHallway,hallwaySelectedFromRoom);
            
            /*Pour la nouvelle salle on lui donne le hallway avec la position relative a la salle*/
            newRoom.setHallway(newHallway);

            /*On verifie si la nouvelle salle n'empiete sur aucune autre salle*/
            bool superimpose = false;
            int room = 0;
            while( !superimpose && room < roomList.Count)
            {
                superimpose = newRoom.isSuperimposeOnAnOtherRoom(roomList[room]);
                room++;
            }

            if (superimpose)
            {
                /*Si on a pas encore atteint le nombre de tentative de creation de salle on relance la fonction*/
                if(numeroTentative < numberAttemptMaxCreationRoom)
                {
                    CreateRoomAndAddToListFromRoom(departRoom,numeroTentative+1);
                } else
                { 
                
                   /*Si on a atteint le nombre maximum de tentative on considere qu'aucune salle ne peut etre creer depuis ce couloir et on ferme le couloir de la salle*/
                   hallwaySelectedFromRoom.setReal(false);
                   int indexDepartRoom = roomList.IndexOf(departRoom);
                   roomList[indexDepartRoom].setHallway(hallwaySelectedFromRoom);
                }
            } else
            {
                /*On rajoute le hallway avec sa position dans le monde dans la hallwayList*/
                hallwayList.Add(newHallway);
                /*On met toutes les positions mondiale des hallway de la salle a jour*/
                newRoom.setAllHalwaysMondialPositions();

                roomList.Add(newRoom);
                //newRoom.PrintRoom();
                //Debug.Log("ofse nouvelle salle : " + ofse + " nouvel hallway : " + premiereCaseNewHallway + " " + derniereCaseNewHallway);
                newRoomCreated = true;

                /*On affecte ensuite le hallway de la salle de depart pourdire qu'il est relie au nouveaux hallway cree*/
                hallwaySelectedFromRoom.addLinkedHallway(newHallway);
                int indexDepartRoom = roomList.IndexOf(departRoom);
                roomList[indexDepartRoom].setHallway(hallwaySelectedFromRoom);
                /*Debug.Log("roomDepartIndex : " + indexDepartRoom);
                Debug.Log("Creation lien entre les deux halways suivants");
                hallwaySelectedFromRoom.printHallway();
                newHallway.printHallway();*/
            }

        }
        
        
        return newRoomCreated;
    }

    int selectRandomRoom()
    {
        bool roomFound = false;
        int roomIndex = -1;
        Room findRoom = new Room();
        /*On creer une liste de salles etant une copie de la liste de salles actuelle*/
        List<Room> roomListToFindRoomWithNotlinkedHallway = new List<Room>();
        for (int i = 0; i < roomList.Count; i++)
        {
            roomListToFindRoomWithNotlinkedHallway.Add(roomList[i]);
        }

        while (!roomFound && roomListToFindRoomWithNotlinkedHallway.Count > 0)
        {
            roomIndex = Random.Range(0, roomListToFindRoomWithNotlinkedHallway.Count-1);
            if(roomListToFindRoomWithNotlinkedHallway[roomIndex].haveAtLeastAnHallwayToCreateAnOtherRoom())
            {
                roomFound = true;
                findRoom = roomListToFindRoomWithNotlinkedHallway[roomIndex];
            }
            roomListToFindRoomWithNotlinkedHallway.RemoveAt(roomIndex);
        }

        if(!roomFound)
        {
            roomIndex = -1;
        }
        
        return roomIndex;
    }

    public void sealAllHalwaysOfAllRooms()
    {
        for (int room= 0; room< roomList.Count;room++)
        {
            roomList[room].sealRoomHalways();
        }
    }

    public int findMaximumDepth()
    {
        int profondeurMax = 0;
        for (int room = 0; room < roomList.Count; room++)
        {
            if(roomList[room].getProfondeur()>profondeurMax)
            {
                profondeurMax = roomList[room].getProfondeur();
            }
        }
        return profondeurMax;
    }

    public void createEndLevelPortal()
    {
        int profondeurMax = 0;
        bool findFinalRoom = false;
        profondeurMax = findMaximumDepth();
        Room finalRoom = new Room();
        int room = 0;
        while(!findFinalRoom && room < roomList.Count)
        {
            if (roomList[room].getProfondeur() == profondeurMax)
            {
                finalRoom = roomList[room];
                profondeurMax = roomList[room].getProfondeur();
                findFinalRoom = true;
            }
            room++;
        }

        boardScript.drawEndPortal(finalRoom.findHallwayEndPortal());
    }

    void InitGame()
    {
        boardScript.GridClear();
        int numberTotalRooms = Random.Range(minNumberRoom, maxNumberRoom);
        CreateRoomAndAddToList(Vector3.zero, boardScript, levelNumber,0);
        for (int numberRooms = 1; numberRooms<numberTotalRooms;numberRooms++)
        {
            int indexRoom = 0;    
            indexRoom = selectRandomRoom();
            if (indexRoom != -1)
            {
                CreateRoomAndAddToListFromRoom(roomList[indexRoom],0);
            }      
        }
        int profondeurMax = findMaximumDepth();
        createEndLevelPortal();
        DrawAllRooms();
        DrawAllHallways();
        sealAllHalwaysOfAllRooms();
    }


}
