using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private bool pressed = false;
    public bool success = false;
    public bool failed = false;
    public bool instantFeedback = false;

    public int moveCnt = 0;

    public int[,] gameArea;
    public List<(int x, int y, bool goal)> allowedMoves;


    public int posX;
    public int posY;

    public float[] coordPosX;
    public float[] coordPosY;

    public List<(int x, int y)> moveTracker = new();

    // Start is called before the first frame update
    void Start()
    {

    }

    // set the characters default starting position
    public void Initialize()
    {
        gameObject.transform.position = new Vector3(coordPosX[posX], coordPosY[posY], 0);
    }

    // Update is called once per frame
    void Update() {
        // honestly ignore this mess. X and Y coordinates are switched so it matches the matrix
        // coordinates. really confusing. dont worry about it, it works
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !success && !failed) {
            if (!pressed) {
                pressed = true;
                if (Input.GetAxisRaw("Horizontal") != 0) {
                    float direction = Check(Input.GetAxisRaw("Horizontal"));

                    if (posY + (int)direction >= 0 && posY + (int)direction < gameArea.GetLength(0) && gameArea[posX, posY + (int)direction] != 0) {
                        posY += (int)direction;
                    } else if (gameArea[posX, posY] == 2) {
                        if (posY == 0) {
                            posY = gameArea.GetLength(0) - 1;
                        } else {
                            posY = 0;
                        }
                    }
                } else {
                    float direction = Check(Input.GetAxisRaw("Vertical"));

                    if (posX + (int)direction * -1 >= 0 && posX + (int)direction * -1 < gameArea.GetLength(1) && gameArea[posX + (int)direction * -1, posY] != 0) {
                        posX += (int)direction * -1;
                    } else if (gameArea[posX, posY] == 3) {
                        if (posX == 0) {
                            posX = gameArea.GetLength(0) - 1;
                        } else {
                            posX = 0;
                        }
                    }
                }
                (int x, int y) prevMove = moveTracker[moveCnt];
                (int x, int y, bool) nextMove = allowedMoves[moveCnt + 1];

                if (instantFeedback) {
                    if (posX == nextMove.x && posY == nextMove.y) {
                        moveCnt++;
                        moveTracker.Add((posX, posY));
                    } else {
                        posX = prevMove.x;
                        posY = prevMove.y;

                        StartCoroutine(Camewa.Shake(0.3f, 0.4f));
                    }
                } else {

                    if (prevMove.x != posX || prevMove.y != posY) {
                        moveCnt++;
                        moveTracker.Add((posX, posY));
                    }

                }
                
                gameObject.transform.position = new Vector3(coordPosX[posY], coordPosY[posX], 0); 
            }

            // set fail/success variables based on the grid or the allowed moves
            if (instantFeedback) {
                if (gameArea[posX, posY] == 69 || allowedMoves[moveCnt].goal){
                    success = true;
                } else if (gameArea[posX, posY] == -1 || allowedMoves[moveCnt].x != posX || allowedMoves[moveCnt].y != posY) {
                    failed = true;
                }
            }
        } else {
            pressed = false;
        }
    }

    // differentiate between left/right and up/down
    private static float Check(float compare) {
        if (compare == 0f){
            return 0f;
        } else if (compare < 0f) {
            return -1f;
        } else {
            return 1f;
        }
    }

    public void resetPlayerPosition()
    {
        posX = moveTracker[moveCnt].x;
        posY = moveTracker[moveCnt].y;

        gameObject.transform.position = new Vector3(coordPosX[posY], coordPosY[posX], 0); ;
    }
}