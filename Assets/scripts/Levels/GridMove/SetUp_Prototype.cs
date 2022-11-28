using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SetUp_Prototype : MonoBehaviour
{
    public Button confirmButton;
    public Button undoBUtton;
    public List<Sprite> objectList = new List<Sprite>();
    public GameObject circle;
    public int level;
    public GameObject levelText;

    public PlayerMovement pm;
    private Colors c = new Colors();
    private bool ignore = false;
    private bool instantFeedback = true;

    // use this matrix to define the game area
    // -1 - death fields, dont touch
    //  0 - not passable (no field)
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

    // this matrix allows you to color the tiles
    // numbers correspond to the index of the color in the color colors array in the colors class
    public int[,] gameAreaColors = new int[5, 5] {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}};

    // define the center points of the tiles and where the character moves to
    private float[] coordPosX = new float[5] { -0.5f, 1.25f, 3f, 4.75f, 6.5f };
    private float[] coordPosY = new float[5] { 3.5f, 1.75f, 0f, -1.75f, -3.5f };

    // define path the user has to take
    // coordinates based on the matrix
    public List<(int x, int y, bool goal)> allowedMoves = new() {
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
        pm.posX = allowedMoves[0].x;
        pm.posY = allowedMoves[0].y;
        pm.coordPosX = coordPosX;
        pm.coordPosY = coordPosY;
        pm.instantFeedback = instantFeedback;
        pm.moveTracker.Add((pm.posX, pm.posY));
        pm.Initialize();

        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
                if (gameArea[x, y] != 0) {
                    AddCircle(x, y);
                }
            }
        }

        if (!instantFeedback) {
            confirmButton.onClick.AddListener(() => buttonClickSubmit());
            undoBUtton.onClick.AddListener(() => buttonClickUndo());
        } else {
            Destroy(confirmButton.gameObject);
            Destroy(undoBUtton.gameObject);
        }
    
    //Set level
    levelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + level.ToString();
    }

    // Update is called once per frame
    // checks if the fail/success variables have been set
    void Update() {
        if (pm.success && !ignore) {
            UnityEngine.Debug.Log("you won!");
            ignore = true;
        } else if (!ignore && (pm.failed || (!instantFeedback && pm.moveCnt >= 15))) {
            UnityEngine.Debug.Log("ah shit you dead noob");
            pm.failed = true;
            ignore = true;
        }
    }

    // adds circles, scales them and assignes them a color
    private void AddCircle(int x, int y) {
        Vector3 circlePos = new Vector3(coordPosX[y], coordPosY[x], 0);
        circle.GetComponent<SpriteRenderer>().sprite = objectList[0];
        GameObject gObj = Instantiate(circle, circlePos, Quaternion.identity) as GameObject;
        Transform t = gObj.transform;
        t.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        
        //if (gameAreaColors[x, y] != 0) {
            gObj.GetComponent<SpriteRenderer>().material.color = c.colors[gameAreaColors[x, y]];
        //}
    }

    private void buttonClickSubmit() {
        if (pm.moveTracker.Count != allowedMoves.Count) {
            pm.failed = true;
            return;
        }

        for (int i = 0; i < pm.moveTracker.Count; i++) {
            if (allowedMoves[i].x != pm.moveTracker[i].x || allowedMoves[i].y != pm.moveTracker[i].y) {
                pm.failed = true;
                return;
            }
        }

        pm.success = true;
    }

    private void buttonClickUndo() {
        if (pm.moveCnt > 0) {
            pm.moveTracker.RemoveAt(pm.moveCnt--);
            pm.resetPlayerPosition();
        }
    }
}