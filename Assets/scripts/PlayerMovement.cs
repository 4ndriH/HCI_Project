using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    private bool pressed = false;
    
    public int[,] gameArea;
    
    public int posX;
    public int posY;

    public float[] coordPosX;
    public float[] coordPosY;


    // Start is called before the first frame update
    void Start() {
       
    }

    public void Initialize() {
        gameObject.transform.position = new Vector3(coordPosX[posX], coordPosY[posY], 0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            if (!pressed) {
                pressed = true;
                if (Input.GetAxisRaw("Horizontal") != 0) {
                    float direction = Check(Input.GetAxisRaw("Horizontal"));

                    if (posY + (int)direction >= 0 && posY + (int)direction < gameArea.GetLength(0) && gameArea[posX, posY + (int)direction] >= 1) {
                        posY += (int)direction;
                    }
                } else {
                    float direction = Check(Input.GetAxisRaw("Vertical"));

                    if (posX + (int)direction * -1 >= 0 && posX + (int)direction * -1 < gameArea.GetLength(1) && gameArea[posX + (int)direction * -1, posY] >= 1) {
                        posX += (int)direction * -1;
                    }
                }
                Vector3 movementDirection = new Vector3(coordPosX[posY], coordPosY[posX], 0);
                Transform t = gameObject.transform;
                t.position = movementDirection;
            }
            
            if (gameArea[posX, posY] == 69) {
                Debug.Log("you won!");
            }
        } else {
            pressed = false;
        }
    }

    private static float Check(float compare) {
        if (compare == 0f)
            return 0f;
              
        else if (compare < 0f)
            return -1f;
              
        else
            return 1f;
    }
}
