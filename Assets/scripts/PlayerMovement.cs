using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    private bool pressed = false;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            float horizontalMovement = 0f;
            float verticalMovement = 0f; 

            if (!pressed) {
                pressed = true;
                if (Input.GetAxisRaw("Horizontal") != 0) {
                    horizontalMovement = 1f * check(Input.GetAxisRaw("Horizontal"));
                } else {
                    verticalMovement = 1f * check(Input.GetAxisRaw("Vertical"));
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
