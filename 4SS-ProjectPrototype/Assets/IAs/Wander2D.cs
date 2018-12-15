using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander2D : MonoBehaviour {
    private Vector3 targetPosition;
    public float movementSpeed = 0.65f;
    private float targetPositionTolerance = 0.5f;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start() {
        minX = -10.0f;
        maxX = 10.0f;
        minY = -10.0f;
        maxY = 10.0f;
        //Get Wander Position
        GetNextPosition();
    }
    
    void Update() {
        if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance) {
            GetNextPosition();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }

    IEnumerator wanderer () {
        while (true) {
            if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance) {
                GetNextPosition();
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;  
        }
    }

    void GetNextPosition() {
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
    }

    private void LateUpdate() {
        
    }
}
