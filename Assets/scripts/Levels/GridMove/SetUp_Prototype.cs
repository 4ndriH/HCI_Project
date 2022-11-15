using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp_Prototype : MonoBehaviour {
    public List<Sprite> objectList = new List<Sprite>();
    public GameObject circle;

    public PlayerMovement pm;

    public int[,] gameArea = new int[5, 5] {
        {1, 0, 0, 0, 0},
        {0, 1, 1, 69, 0},
        {1, 1, 1, 1, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 0, 0, 0}};

    public float[] coordPosX = new float[5] { -2f, -1f, 0f, 1f, 2f };
    public float[] coordPosY = new float[5] { 2f, 1f, 0f, -1f, -2f };

    // Start is called before the first frame update
    void Start() {
        pm.gameArea = gameArea;
        pm.posX = 2;
        pm.posY = 2;
        pm.coordPosX = coordPosX;
        pm.coordPosY = coordPosY;
        pm.Initialize();

        for (int x = 0; x< 5; x++) {
            for (int y = 0; y < 5; y++) {
                if (gameArea[x, y] >= 1) {
                    AddCircle(coordPosX[y], coordPosY[x]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void AddCircle(float x, float y) {
        Vector3 circlePos = new Vector3(x, y, 0);
        circle.GetComponent<SpriteRenderer>().sprite = objectList[0];
        GameObject gObj = Instantiate(circle, circlePos, Quaternion.identity) as GameObject;
        Transform t = gObj.transform;
        t.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
