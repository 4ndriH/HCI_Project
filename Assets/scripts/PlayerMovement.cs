using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    private bool pressed = false;
    private int[,] gameArea = new int[5, 5] { 
        {0, 0, 1, 1, 0}, 
        {0, 1, 1, 1, 0}, 
        {0, 1, 1, 1, 0}, 
        {0, 0, 1, 0, 0},
        {0, 0, 1, 0, 0}};
    
    private int xIdx = 2;
    private int yIdx = 2; 

    
    // Start is called before the first frame update
    void Start() {
        gameObject.transform.position = new Vector3(0, 0, 0);
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

                    if (yIdx + (int)direction >= 0 && yIdx + (int)direction < gameArea.GetLength(0) && gameArea[xIdx, yIdx + (int)direction] == 1) {
                        horizontalMovement = 1f * direction;
                        yIdx += (int)direction;
                    }
                } else {
                    float direction = check(Input.GetAxisRaw("Vertical"));

                    if (xIdx + (int)direction * -1 >= 0 && xIdx + (int)direction * -1 < gameArea.GetLength(1) && gameArea[xIdx + (int)direction * -1, yIdx] == 1) {
                        verticalMovement = 1f * direction;
                        xIdx += (int)direction * -1;
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
