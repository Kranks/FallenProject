using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway {

    /*Les deux vecteurs qui retiennent les position de fin et de depart du couloir de maniere locale a la salle*/
    private Vector3 firstLocalSquare;
    private Vector3 lastLocalSquare;
    /*Les deux vecteurs qui retiennent les position de fin et de depart du couloir dans le monde*/
    private Vector3 firstWorldSquare;
    private Vector3 lastWorldSquare;
    /*booleen qui marque si le couloir a ete creer ou non*/
    private bool real = false;
    /*booleen qui permet de savoir si il est lie a un autre couloir ou non*/
    private bool linked = false;
    /*booleen qui sert a savoir si le couloir doit etre dessiner depuis ce cote
     pour que les couloirs ne soit pas dessine 2 fois
    */
    private bool drawable = false;
    /**/
    private bool isEndPortal = false;
    /*hallway auquel le hallway est liee*/
    private Hallway linkedHallway;
    /*Entier qui defini si le hallway est sur l'axe 0 vertical ou 1 horizontal*/
    int axe;
    /*Entier qui defini la direction du hallway 
     * 0 left et 1 right si l'axe est vertical
     * 0 down et 1 up si l'axe est horizontal
    */
    int direction;
    
    

    public Hallway()
    {
        real = false;
        linked = false;
        drawable = false;
        isEndPortal = false;
    }
    public Hallway(Vector3 vecLocal1, Vector3 veclocal2,Vector3 vecWorld1, Vector3 vecWorld2,int ax, int dir)
    {
        firstLocalSquare = vecLocal1;
        lastLocalSquare = veclocal2;
        firstWorldSquare = vecWorld1;
        lastWorldSquare = vecWorld2;
        axe = ax;
        direction = dir; 
        real = true;
    }

    public Hallway(Vector3 vecLocal1, Vector3 veclocal2, Vector3 vecWorld1, Vector3 vecWorld2, int ax, int dir, Hallway hallwayToLink)
    {
        firstLocalSquare = vecLocal1;
        lastLocalSquare = veclocal2;
        firstWorldSquare = vecWorld1;
        lastWorldSquare = vecWorld2;
        axe = ax;
        direction = dir;
        real = true;
        linked = true;
        drawable = true;
        linkedHallway = hallwayToLink;
    }

    public int hallwaySize()
    {
        int size = -1;
        if(real)
        {
            if (axe == 0) {
                size = (int)(lastLocalSquare.y - firstLocalSquare.y);
            } else
            {
                size = (int)(lastLocalSquare.x - firstLocalSquare.x);
            }
        }

        return size;
    }

    public void addLinkedHallway(Hallway HallwayToLink)
    {
        linkedHallway = HallwayToLink;
        linked = true;
    }

    public bool VectorIsInHallWay(Vector3 vec)
    {
        bool IsInHallway = false;
        if (real)
        { 
            if (firstLocalSquare.x <= vec.x && vec.x <= lastLocalSquare.x)
            {
                if (firstLocalSquare.y <= vec.y && vec.y <= lastLocalSquare.y)
                {
                    IsInHallway = true;
                }
            }
        }
        return IsInHallway;
    }

    public void printHallway()
    {
        if (real)
        {
            Debug.Log("premiere case locale: " + firstLocalSquare + " derniere case locale" + lastLocalSquare);
            Debug.Log("premiere case mondiale: " + firstWorldSquare + " derniere case mondiale" + lastWorldSquare);
            Debug.Log(" real : "+real+" linked : "+ linked+ " drawable : " +drawable);
        }
    }

    public bool HallwayIsLinked()
    {
        return linked;
    }

    public Hallway getLinkedHallway()
    {
        return linkedHallway;
    }

    public bool isReal()
    {
        return real;
    }

    public Vector3 getFirstLocalSquare()
    {
        return firstLocalSquare;
    }

    public Vector3 getLastLocalSquare()
    {
        return lastLocalSquare;
    }

    public Vector3 getFirstWorldSquare()
    {
        return firstWorldSquare;
    }

    public Vector3 getLastWorldSquare()
    {
        return lastWorldSquare;
    }

    /*ces fonction permettent de placer mondialement un hallway a partir d'un offset*/
    public void setWorldPositionHallway(Vector3 offset)
    {
       firstWorldSquare = firstLocalSquare + offset;
       lastWorldSquare = lastLocalSquare + offset;
    }

    public int getAxe()
    {
        return axe;
    }

    public int getDirection()
    {
        return direction;
    }

    /*Pour le portail de fin on fait comme si il n'existait pas pour eviter les problemes avec d'autres fonctions*/
    public void setEndPortal()
    {
        isEndPortal = true;
    }

    public bool getIsEndPortal()
    {
        return isEndPortal;
    }

    public void setReal(bool choice)
    {
        real = choice;
    }
}
