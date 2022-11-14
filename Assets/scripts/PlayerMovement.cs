using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    private bool pressed = false;
    
    public int[,] gameArea;
    
    public int startX;
    public int startY;

    public float[] coordPosX;
    public float[] coordPosY;


    // Start is called before the first frame update
    void Start() {
       
    }

    public void initialize() {
        gameObject.transform.position = new Vector3(coordPosX[startY], coordPosY[startX], 0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            float horizontalMovement = 0f;
            float verticalMovement = 0f; 

            if (!pressed) {
                pressed = true;
                if (Input.GetAxisRaw("Horizontal") != 0) {
                    float direction = check(Input.GetAxisRaw("Horizontal"));

                    if (startY + (int)direction >= 0 && startY + (int)direction < gameArea.GetLength(0) && gameArea[startX, startY + (int)direction] == 1) {
                        horizontalMovement = 1f * direction;
                        startY += (int)direction;
                    }
                } else {
                    float direction = check(Input.GetAxisRaw("Vertical"));

                    if (startX + (int)direction * -1 >= 0 && startX + (int)direction * -1 < gameArea.GetLength(1) && gameArea[startX + (int)direction * -1, startY] == 1) {
                        verticalMovement = 1f * direction;
                        startX += (int)direction * -1;
                    }
                }
            }

            Vector3 movementDirection = new Vector3(horizontalMovement, verticalMovement, 0);

            gameObject.transform.Translate(movementDirection);
        } else {
            pressed = false;
        }
    }

    public static float check(float compare) {
        if (compare == 0f)
            return 0f;
              
        else if (compare < 0f)
            return -1f;
              
        else
            return 1f;
    }
}
