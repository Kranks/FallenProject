using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {


    private int columns = 8;
    private int rows = 8;
    private Vector3 offset;
    private BoardManager boardScript;
    private int level;
    private int profondeur;
    /*On cree une 2 tableau de 2 Hallway
     * un tableau horizontal avec  
     * 0 : left
     * 1 : right
     * un tableau vertical
     * 0 : down
     * 1 : up
     */
    private Hallway[] listHorizontalHallways = new Hallway[2];
    private Hallway[] listVerticalHallways = new Hallway[2];

    public Room()
    {

    }

    public Room(Vector3 ofse, BoardManager board, int lev, int pro, int minSize, int maxSize)
    {
        columns = Random.Range(minSize, maxSize);
        rows = Random.Range(minSize, maxSize);
        offset = ofse;
        boardScript = board;
        level = lev;
        profondeur = pro;
        /*On met les couloirs a vide au depart
         * on peut utiliser la taille d'un seul tableau car les deux tableaux d'hallway sont de la meme taille
        */
        for (int h = 0; h < listHorizontalHallways.Length; h++)
        {
            listHorizontalHallways[h] = new Hallway();
            listVerticalHallways[h] = new Hallway();
        }

        createRandomHallways();
    }

    public void personalizeBoard()
    {
        boardScript.SetColumns(columns);
        boardScript.SetRows(rows);
        boardScript.SetOffset(offset);
    }

    public void createRandomHallways()
    {
        /*Correspond au pourcentage de chance qu'un hallway soit cree*/
        int pourcentageCreationHallway = 100;
        /*On utilise cet int pour faire la randomisation de creation des hallways*/
        int randomPourcentage = Random.Range(0, 100);
        /*Correspond a la taille d'un hallway*/
        int hallwaySize = 0;
        /*Coorespond a la position de depart du hallway 
         * soit en x (pour les horizontalHallways)
         * soit en y (pour les verticalHallways)
         */
        int position = 0;


        for (int hallwayHorizontal = 0; hallwayHorizontal < listHorizontalHallways.Length; hallwayHorizontal++)
        {
            /*Debug.Log("pourcentageCreationHallway : " + pourcentageCreationHallway + " pourcentageCreationHallway : "+randomPourcentage);*/
            if (pourcentageCreationHallway >= randomPourcentage)
            {
                hallwaySize = Random.Range(4, rows);
                position = Random.Range(0, (rows - hallwaySize));
                /*
                Debug.Log("le nombre de colonne pour la classe Room est : " + columns);
                Debug.Log("le nombre de ligne pour la classe Room est : " + rows);
                Debug.Log("la taille du couloir Horizontal est comprise entre 2 et " + (columns - 2));
                Debug.Log("la position du couloir Horizontal est comprise entre 0 et " + (hallwaySize - 2));
                */

                /*On creer des vecteur3 qui correpondront aux premiere et derniere cases locales du hallway cree */
                Vector3 firstLocalSquare;
                Vector3 lastLocalSquare;
                if (hallwayHorizontal == 0)
                {
                    firstLocalSquare = new Vector3(-1, position, 0);
                    lastLocalSquare = new Vector3(-1, position + hallwaySize, 0);
                    listHorizontalHallways[hallwayHorizontal] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 0, hallwayHorizontal);
                } else
                {
                    firstLocalSquare = new Vector3(columns, position, 0);
                    lastLocalSquare = new Vector3(columns, position + hallwaySize, 0);
                    listHorizontalHallways[hallwayHorizontal] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 0, hallwayHorizontal);
                }


            }
            randomPourcentage = Random.Range(0, 100);
        }

        for (int hallwayVertical = 0; hallwayVertical < listVerticalHallways.Length; hallwayVertical++)
        {
            /*Debug.Log("pourcentageCreationHallway : " + pourcentageCreationHallway + " pourcentageCreationHallway : " + randomPourcentage);*/
            if (pourcentageCreationHallway >= randomPourcentage)
            {

                hallwaySize = Random.Range(2, columns);
                position = Random.Range(0, (columns - hallwaySize));
                /*
                Debug.Log("le nombre de colonne pour la classe Room est : " + columns);
                Debug.Log("le nombre de ligne pour la classe Room est : " + rows);
                Debug.Log("la taille du couloir Vertical est comprise entre 2 et " + (columns-2));
                Debug.Log("la position du couloir Vertical est comprise entre 0 et " + (hallwaySize-2));
                */

                /*On creer des vecteur3 qui correpondront aux premiere et derniere cases locales du hallway cree */
                Vector3 firstLocalSquare;
                Vector3 lastLocalSquare;

                if (hallwayVertical == 0)
                {
                    firstLocalSquare = new Vector3(position, -1, 0);
                    lastLocalSquare = new Vector3(position + hallwaySize, -1, 0);
                    listVerticalHallways[hallwayVertical] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 1, hallwayVertical);
                }
                else
                {
                    firstLocalSquare = new Vector3(position, rows, 0);
                    lastLocalSquare = new Vector3(position + hallwaySize, rows, 0);
                    listVerticalHallways[hallwayVertical] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 1, hallwayVertical);
                }

            }
            randomPourcentage = Random.Range(0, 100);

        }

    }

    public void setHallway(Hallway hallway)
    {
        /*axe=0 correspond a l'axe vertical*/
        if (hallway.getAxe() == 0) {
            /*direction=0 correspond a left*/
            if (hallway.getDirection() == 0)
            {
                listHorizontalHallways[0] = hallway;
            }
            /*direction=1 correspond a right*/
            else
            {
                listHorizontalHallways[1] = hallway;
            }

        }
        /*axe=1 correspond a l'axe vertical*/
        else
        {
            /*direction=0 correspond a down*/
            if (hallway.getDirection() == 0)
            {
                listVerticalHallways[0] = hallway;
            }
            /*direction=1 correspond a up*/
            else
            {
                listVerticalHallways[1] = hallway;
            }
        }
    }

    /*Cette fonction permet de selectionner aleatoirement un hallway parmis les 4 de la salle*/
    public Hallway selectRandomLinkableHallway()
    {
        /*On creer un list d'hallways avec
         0 : left
         1 : right
         2 : down
         3 : up
         */
        Hallway selectedHallway = new Hallway();
        List<Hallway> listHallways = new List<Hallway>();
        listHallways.Add(listHorizontalHallways[0]);
        listHallways.Add(listHorizontalHallways[1]);
        listHallways.Add(listVerticalHallways[0]);
        listHallways.Add(listVerticalHallways[1]);

        bool hallwayFound = false;
        int hallwayIndex = 0;
        while (!hallwayFound && listHallways.Count > 0)
        {
            hallwayIndex = Random.Range(0, listHallways.Count);
            selectedHallway = listHallways[hallwayIndex];
            listHallways.RemoveAt(hallwayIndex);
            if (selectedHallway.isReal() && !selectedHallway.HallwayIsLinked())
            {
                /*Debug.Log("Hallway choisi est : ");
                selectedHallway.printHallway();*/
                hallwayFound = true;
            }
            //Debug.Log("listHallwaus count : " + listHallways.Count);
        }
        return selectedHallway;
    }

    /*Cette fonction regarde si la salle possede un hallway qui existe mais qui n'est 
     pas relie a un autre hallway de maniere a pouvoir creer une nouvelle salle
     reliee a cet hallway
    */
    public bool haveAtLeastAnHallwayToCreateAnOtherRoom()
    {
        bool anHalwayIsRealButNotLinked = false;
        if (listHorizontalHallways[0].isReal() && !listHorizontalHallways[0].HallwayIsLinked())
        {
            anHalwayIsRealButNotLinked = true;
        } else if (listHorizontalHallways[1].isReal() && !listHorizontalHallways[1].HallwayIsLinked())
        {
            anHalwayIsRealButNotLinked = true;
        } else if (listVerticalHallways[0].isReal() && !listVerticalHallways[0].HallwayIsLinked())
        {
            anHalwayIsRealButNotLinked = true;
        } else if (listVerticalHallways[1].isReal() && !listVerticalHallways[1].HallwayIsLinked())
        {
            anHalwayIsRealButNotLinked = true;
        }
        return anHalwayIsRealButNotLinked;
    }

    /*Lorsque les hallways sont creer au depart l'offset n'existe pas forcement c'est pourquoi cette fonction
     permet de remmettre a jour toutes les positions mondiales des hallways a partir de l'offset*/

    public void setAllHalwaysMondialPositions()
    {
        listHorizontalHallways[0].setWorldPositionHallway(offset);
        listHorizontalHallways[1].setWorldPositionHallway(offset);
        listVerticalHallways[0].setWorldPositionHallway(offset);
        listVerticalHallways[1].setWorldPositionHallway(offset);
    }


    /*Cette fonction permet de remplacer les hallways qui ne sont pas relie a d'autre hallways par des murs*/
    public void sealRoomHalways()
    {
        if ((!listHorizontalHallways[0].getIsEndPortal()) && (listHorizontalHallways[0].isReal() && !listHorizontalHallways[0].HallwayIsLinked()))
        {
            boardScript.fillHallway(listHorizontalHallways[0]);
        }
        if ((!listHorizontalHallways[1].getIsEndPortal()) && (listHorizontalHallways[1].isReal() && !listHorizontalHallways[1].HallwayIsLinked()))
        {
            boardScript.fillHallway(listHorizontalHallways[1]);
        }
        if ((!listVerticalHallways[0].getIsEndPortal()) && (listVerticalHallways[0].isReal() && !listVerticalHallways[0].HallwayIsLinked()))
        {
            boardScript.fillHallway(listVerticalHallways[0]);
        }
        if ((!listVerticalHallways[1].getIsEndPortal()) && (listVerticalHallways[1].isReal() && !listVerticalHallways[1].HallwayIsLinked()))
        {
            boardScript.fillHallway(listVerticalHallways[1]);
        }
    }

    /*Cette fonction permet pour une salle donner de trouver l'endroit ou se trouvera le portail de fin de niveau*/
    public Hallway findHallwayEndPortal()
    {
        Hallway endPortal = new Hallway();
        int hallwaySize = Random.Range(1, 1);
        int position = Random.Range(0, (rows - hallwaySize));
        Vector3 firstLocalSquare = new Vector3();
        Vector3 lastLocalSquare = new Vector3();
        /*On peut dessiner un portail de fin sur un hallways si il ne sont pas reel ou si ils sont reel mais relie a aucun autre hallway*/
        
        if ((!listVerticalHallways[0].isReal()) ||  (listVerticalHallways[0].isReal() && !listVerticalHallways[0].HallwayIsLinked()))
        {
            position = Random.Range(0, (columns - hallwaySize));
            firstLocalSquare = new Vector3(position, -1, 0);
            lastLocalSquare = new Vector3(position + hallwaySize, -1, 0);
            listVerticalHallways[0] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 1, 0);
            listVerticalHallways[0].setEndPortal();
            endPortal = listVerticalHallways[0];
        }
        else if ((!listVerticalHallways[1].isReal()) ||  (listVerticalHallways[1].isReal() && !listVerticalHallways[1].HallwayIsLinked()))
        {
            position = Random.Range(0, (columns - hallwaySize));
            firstLocalSquare = new Vector3(position, rows, 0);
            lastLocalSquare = new Vector3(position + hallwaySize, rows, 0);
            listVerticalHallways[1] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 1, 1);
            listVerticalHallways[1].setEndPortal();
            endPortal = listVerticalHallways[1];
        }
         else if ((!listHorizontalHallways[0].isReal()) || (listHorizontalHallways[0].isReal() && !listHorizontalHallways[0].HallwayIsLinked()))
        {
            position = Random.Range(0, (rows - hallwaySize));
            firstLocalSquare = new Vector3(-1, position, 0);
            lastLocalSquare = new Vector3(-1, position + hallwaySize, 0);
            listHorizontalHallways[0] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 0, 0);
            listHorizontalHallways[0].setEndPortal();
            endPortal = listHorizontalHallways[0];
        }
        else if ((!listHorizontalHallways[1].isReal()) || (listHorizontalHallways[1].isReal() && !listHorizontalHallways[1].HallwayIsLinked()))
        {
            position = Random.Range(0, (rows - hallwaySize));
            firstLocalSquare = new Vector3(columns, position, 0);
            lastLocalSquare = new Vector3(columns, position + hallwaySize, 0);
            listHorizontalHallways[1] = new Hallway(firstLocalSquare, lastLocalSquare, firstLocalSquare + offset, lastLocalSquare + offset, 0, 1);
            listHorizontalHallways[1].setEndPortal();
            endPortal = listHorizontalHallways[1];
        }
        return endPortal;
    }

    /*Cette fonction permet de savoir si une salle empiette sur une autre salle*/
    public bool isSuperimposeOnAnOtherRoom(Room roomTest)
    {
        bool isSuperimpose = false;
        /*On recupere les premieres cases (offset) et derniere case dessiner qui ne sont pas des murs des deux salles pour recuper leurs positions x et y min et max*/
        int minXRoomTest = (int)roomTest.getOffset().x;
        int minYRoomTest = (int)roomTest.getOffset().y;
        int maxXRoomTest = (int)roomTest.getLastSquare().x;
        int maxYRoomTest = (int)roomTest.getLastSquare().y;

        int minXRoom = (int)getOffset().x;
        int minYRoom = (int)getOffset().y;
        int maxXRoom = (int)getLastSquare().x;
        int maxYRoom = (int)getLastSquare().y;


        /*On itere sur les x de minXRoom a MaxXRoom*/
        for (int x = minXRoom; x <= maxXRoom; x++)
        {
            /*Si ce x est situer entre minXRoomTest et maxXRoomTest*/
            if(minXRoomTest<=x && x<= maxXRoomTest)
            {
                /*Alors on itere sur les y pour voir si c'est la cas en y aussi*/
                for(int y = minYRoom; y <= maxYRoom; y++)
                {
                    if (minYRoomTest<=y && y<= maxYRoomTest)
                    {
                        /*Si il y a couple (x,y) de la salle qui compris dans les min et max de x et y de la salle avec laquelle on teste la superposition
                         alors c'est qu'il y a superposition entre les deux salles*/
                        isSuperimpose = true;
                    }
                }
            }
        }
        return isSuperimpose;
    }

    public void PrintHallHallway()
    {
        Debug.Log("Left hallway : ");
        listHorizontalHallways[0].printHallway();
        Debug.Log("Right hallway : ");
        listHorizontalHallways[1].printHallway();
        Debug.Log("Down hallway : ");
        listVerticalHallways[0].printHallway();
        Debug.Log("Up hallway : ");
        listVerticalHallways[1].printHallway();

    }

    public void PrintRoom()
    {
        Debug.Log("Room rows : " + rows);
        Debug.Log("Room columns : " + columns);
        Debug.Log("Offset : " + offset);
        PrintHallHallway();
    }

    public void DrawRoom()
    {
        boardScript.GridClear();
        personalizeBoard();
        boardScript.SetupScene(level,listHorizontalHallways[0], listHorizontalHallways[1], listVerticalHallways[0], listVerticalHallways[1]);
        /*Debug.Log("le nombre de colonne pour la classe Room est: " + columns);
        Debug.Log("le nombre de ligne pour la classe Room est : " + rows);
        PrintHallHallway();*/
    }

    public void setOffset(Vector3 ofse)
    {
        offset = ofse;
    }

    public int getColumns()
    {
        return columns;
    }

    public int getRows()
    {
        return rows;
    }

    /*Cette fonction permet de recuperer la derniere case dessiner de la salle (le coin en haut a droite qui n'est pas un mur)*/
    public Vector3 getLastSquare()
    {
        float x = offset.x + columns ;
        float y = offset.y + rows ;
        return new Vector3(x, y, 0);
    }

    public Vector3 getOffset()
    {
        return offset;
    }

    public int getProfondeur()
    {
        return profondeur;
    }

    public void setRows(int min, int max)
    {
        rows = Random.Range(min, max);
    }

    public void setColumns(int min, int max)
    {
        columns = Random.Range(min, max);
    }
}
