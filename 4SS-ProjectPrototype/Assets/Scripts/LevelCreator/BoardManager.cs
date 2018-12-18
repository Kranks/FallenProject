using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Math = System.Math;
using Random = UnityEngine.Random;
public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    private int columns = 0;
    private int rows = 0;
    private Vector3 offset;


    public Count wallCount = new Count(5, 9);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] ennemyTiles;
    public GameObject[] horizontalOuterWallTiles;
    public GameObject[] verticalOuterWallTiles;
    public GameObject[] endPortal;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList(Vector3 ofse)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                gridPositions.Add(new Vector3(x + ofse.x, y + ofse.y, 0f));
            }
        }
        /*Debug.Log("A la creation de la salle le gridPositions est a : "+gridPositions.Count+" avec " +columns +" colonne et "+ rows +" ligne" );*/
    }

    void BoardSetUp(Vector3 ofse, Hallway left, Hallway right, Hallway up, Hallway down)
    {
        boardHolder = new GameObject("Board").transform;
        /*On veut dessiner une salle en prenant en compte les bords situes aux -1 et nombres de lignes ou collones +1*/
        /*On itere sur toutes les collones*/
        for (int x = -1; x < columns + 1; x++)
        {
            /*Puis sur toutes les lignes */
            for (int y = -1; y < rows + 1; y++)
            {
                /*On creer un objet a instancier que l'on initialise en tant que sol*/
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length - 1)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    /*Si on est situe sur un des bord qui ne soit pas inclus dans les couloirs, on intancie alors un mur externe*/
                    if (!left.VectorIsInHallWay(position) && !right.VectorIsInHallWay(position) && !up.VectorIsInHallWay(position) && !down.VectorIsInHallWay(position))
                    {
                        /*On verifie si il s'agit de mur horizontaux ou verticaux*/
                        if (x == -1 || x == columns)
                        {
                            toInstantiate = verticalOuterWallTiles[Random.Range(0, verticalOuterWallTiles.Length - 1)];
                        } else
                        {
                            toInstantiate = horizontalOuterWallTiles[Random.Range(0, horizontalOuterWallTiles.Length - 1)];
                        }
                    }
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x + ofse.x, y + ofse.y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, ((gridPositions.Count) - 1));
        /*Debug.Log("GridPositions.Count : " + gridPositions.Count);*/
        Vector3 randomPosition = gridPositions[randomIndex];
        if (randomPosition != new Vector3(0, 0, 0))
        {
            gridPositions.RemoveAt(randomIndex);
        } else
        {
            RandomPosition();
        }
        return randomPosition;
    }

    void layoutObjectAtRandom(GameObject[] tileArray, int min, int max)
    {
        int objectCount = Random.Range(min, max + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length - 1)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }


    /*Permet de nettoyer la grille*/
    public void GridClear()
    {
        gridPositions.Clear();
    }

    public void SetupScene(int level, Hallway left, Hallway right, Hallway up, Hallway down)
    {
        BoardSetUp(offset, left, right, up, down);
        InitializeList(offset);
        layoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        int ennemyCount = Random.Range(level*3,level*5);
        layoutObjectAtRandom(ennemyTiles, ennemyCount, ennemyCount);
    }

    /*Dessine un Hallway complet a partir de deux partie de hallways*/
    public void drawFullHallWay(Hallway h1, Hallway h2)
    {
        int xHallwayMin = (int)Math.Min(h1.getFirstWorldSquare().x, h2.getFirstWorldSquare().x);
        int yHallwayMin = (int)Math.Min(h1.getFirstWorldSquare().y, h2.getFirstWorldSquare().y);
        int xHallwayMax = (int)Math.Max(h1.getLastWorldSquare().x, h2.getLastWorldSquare().x);
        int yHallwayMax = (int)Math.Max(h1.getLastWorldSquare().y, h2.getLastWorldSquare().y);

        /*Si le couloir est sur l'axe vertical (gauche,droite)*/
        if (h1.getAxe() == 0)
        {
            for (int x = xHallwayMin + 1; x <= xHallwayMax - 1; x++)
            {
                for (int y = yHallwayMin - 1; y <= yHallwayMax + 1; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0));
                    /*On creer un objet a instancier que l'on initialise en tant que sol*/
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length - 1)];
                    /*On creer un mur de couloir si on est a l'exterieur de celui ci dans l'axe du couloir*/
                    if (y == yHallwayMin - 1 || y == yHallwayMax + 1)
                    {
                        toInstantiate = verticalOuterWallTiles[Random.Range(0, verticalOuterWallTiles.Length - 1)];
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                }
            }
        } else /*on est dans l'axe horizontal(haut,bas)*/
        {
            for (int x = xHallwayMin - 1; x <= xHallwayMax + 1; x++)
            {
                for (int y = yHallwayMin + 1; y <= yHallwayMax - 1; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0));
                    /*On creer un objet a instancier que l'on initialise en tant que sol*/
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length - 1)];
                    /*On creer un mur de couloir si on est a l'exterieur de celui ci dans l'axe du couloir*/
                    if (x == xHallwayMin - 1 || x == xHallwayMax + 1)
                    {
                        toInstantiate = horizontalOuterWallTiles[Random.Range(0, horizontalOuterWallTiles.Length - 1)];
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                }
            }
        }
    }

    /*Cette fonction permet de dessiner le portail de fin 
     * menant d'un niveau a un autre*/
     public void drawEndPortal(Hallway endPortalHallway)
    {
        for (int x = (int)endPortalHallway.getFirstWorldSquare().x; x <= (int)endPortalHallway.getLastWorldSquare().x; x++)
        {
            for (int y = (int)endPortalHallway.getFirstWorldSquare().y; y <= (int)endPortalHallway.getLastWorldSquare().y; y++)
            {
                if(x == (int)endPortalHallway.getFirstWorldSquare().x)
                {
                    gridPositions.Add(new Vector3(x, y, 0));
                    GameObject toInstantiate = endPortal[Random.Range(0, endPortal.Length - 1)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                }
                
            }
        }
    }

    /*Cette fonction permet de remplace un debut de couloir par un mur si jamais le niveau  
     * a fini d'être dessiner avec des debut de couloir restant*/
    public void fillHallway(Hallway h1)
    {
        for (int x = (int)h1.getFirstWorldSquare().x; x <= (int)h1.getLastWorldSquare().x; x++)
        {
            for (int y = (int)h1.getFirstWorldSquare().y; y <= (int)h1.getLastWorldSquare().y; y++)
            {
                /*On verifie s on est sur un mur vertical ou horizontal*/
                if (x == (int)h1.getFirstWorldSquare().x || x == (int)h1.getLastWorldSquare().x)
                {
                    gridPositions.Add(new Vector3(x, y, 0));
                    GameObject toInstantiate = verticalOuterWallTiles[Random.Range(0, verticalOuterWallTiles.Length - 1)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                } else
                {
                    gridPositions.Add(new Vector3(x, y, 0));
                    GameObject toInstantiate = horizontalOuterWallTiles[Random.Range(0, horizontalOuterWallTiles.Length - 1)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                }
            }
        }
    }



    public int getColums()
    {
        return columns;
    }

    public int getRows()
    {
        return rows;
    }

    public void SetOffset(Vector3 vec)
    {
        offset = vec;
    }

    public void SetColumns(int col)
    {
        columns = col;
    }

    public void SetRows(int row)
    {
        rows = row;
    }
}
