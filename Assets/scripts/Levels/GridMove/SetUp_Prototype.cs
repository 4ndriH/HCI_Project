using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SetUp_Prototype : MonoBehaviour {
    public List<Sprite> objectList = new List<Sprite>();
    public GameObject circle;

    public PlayerMovement pm;
    private Colors c = new Colors();
    private bool ignore = false;

    // -1 - death fields, dont touch
    //  0 - not passable
    //  1 - normal field
    //  2 - teleporter to the other side (horizontal)
    //  3 - teleporter to the other side (vertical)
    // 69 - goal

    public int[,] gameArea = new int[5, 5] {
        {1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1}};

    public int[,] gameAreaColors = new int[5, 5] {
        {0, 0, 0, 2, 0},
        {0, 0, 0, 0, 0},
        {2, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}};

    public float[] coordPosX = new float[5] { -2f, -1f, 0f, 1f, 2f };
    public float[] coordPosY = new float[5] { 2f, 1f, 0f, -1f, -2f };

    public List<(int x, int y, bool goal)> allowedMoves = new () {
        (2, 2, false),
        (1, 2, false),
        (1, 3, false),
        (2, 3, false),
        (3, 3, false),
        (3, 2, false),
        (3, 1, false),
        (2, 1, false),
        (1, 1, false),
        (0, 1, false),
        (0, 2, false),
        (0, 3, false),
        (0, 4, true)
    };

    // Start is called before the first frame update
    void Start() {
        pm.gameArea = gameArea;
        pm.allowedMoves = allowedMoves;
        pm.posX = 2;
        pm.posY = 2;
        pm.coordPosX = coordPosX;
        pm.coordPosY = coordPosY;
        pm.Initialize();

        for (int x = 0; x< 5; x++) {
            for (int y = 0; y < 5; y++) {
                if (gameArea[x, y] != 0) {
                    AddCircle(x, y);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (pm.success && !ignore){
            UnityEngine.Debug.Log("you won!");
            ignore = true;
        } else if (pm.failed && !ignore) {
            UnityEngine.Debug.Log("ah shit you dead noob");
            ignore = true;
        }
    }

    private void AddCircle(int x, int y) {
        Vector3 circlePos = new Vector3(coordPosX[y], coordPosY[x], 0);
        circle.GetComponent<SpriteRenderer>().sprite = objectList[0];
        GameObject gObj = Instantiate(circle, circlePos, Quaternion.identity) as GameObject;
        Transform t = gObj.transform;
        t.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        if (gameAreaColors[x, y] != 0) {
            gObj.GetComponent<SpriteRenderer>().material.color = c.colors[gameAreaColors[x, y]];
        }
    }
}
