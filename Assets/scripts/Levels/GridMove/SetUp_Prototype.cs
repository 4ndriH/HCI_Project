using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp_Prototype : MonoBehaviour {
    public List<Sprite> objectList = new List<Sprite>();
    public GameObject circle;

    public PlayerMovement pm;

    public int[,] gameArea = new int[5, 5] {
        {0, 0, 1, 1, 0},
        {0, 1, 1, 1, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 1, 0, 0}};

    public float[] coordPosX = new float[5] { -2f, -1f, 0f, 1f, 2f };
    public float[] coordPosY = new float[5] { 2f, 1f, 0f, -1f, -2f };

    // Start is called before the first frame update
    void Start() {
        pm.gameArea = gameArea;
        pm.startX = 2;
        pm.startY = 3;
        pm.coordPosX = coordPosX;
        pm.coordPosY = coordPosY;
        pm.initialize();

        for (int i = 0; i< 5; i++) {
            for (int j = 0; j < 5; j++) {
                if (gameArea[i, j] == 1) {
                    addCircle(coordPosX[j], coordPosY[i]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void addCircle(float x, float y) {
        Vector3 circlePos = new Vector3(x, y, 0);
        circle.GetComponent<SpriteRenderer>().sprite = objectList[0];
        GameObject gObj = Instantiate(circle, circlePos, Quaternion.identity) as GameObject;
        Transform t = gObj.transform;
        t.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
