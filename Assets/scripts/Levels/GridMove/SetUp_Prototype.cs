using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SetUp_Prototype : MonoBehaviour
{
    public Button confirmButton;
    public Button undoBUtton;
    public List<Sprite> objectList = new List<Sprite>();
    public GameObject circle;
    private int level;
    public GameObject levelText;
    public Image TaskDescription;
    public PlayerMovement pm;
    private Colors c = new Colors();
    private bool ignore = false;
    private bool instantFeedback = Config.getInstantFeedback();
    //private GatherData gd = new GatherData();
    public GameObject nextButton;
    public GameObject winText;
    public GameObject retryButton;
    public GameObject lossText;

    private List<GameObject> circleList = new List<GameObject>();

    // use this matrix to define the game area
    // -1 - death fields, dont touch
    //  0 - not passable (no field)
    //  1 - normal field
    //  2 - teleporter to the other side (horizontal)
    //  3 - teleporter to the other side (vertical)
    // 69 - goal
    public int[,] gameArea;

    // this matrix allows you to color the tiles
    // numbers correspond to the index of the color in the color colors array in the colors class
    public int[,] gameAreaColors;

    // define the center points of the tiles and where the character moves to
    private float[] coordPosX = new float[5] { -0.5f, 1.25f, 3f, 4.75f, 6.5f };
    private float[] coordPosY = new float[5] { 3.5f, 1.75f, 0f, -1.75f, -3.5f };

    // define path the user has to take
    // coordinates based on the matrix
    public List<(int x, int y, bool goal)> allowedMoves;

    // Start is called before the first frame update
    void Start() {
        LevelLoader();
        
        GatherData.startLevel();
    }

    // Update is called once per frame
    // checks if the fail/success variables have been set
    void Update() {
        if (pm.success && !ignore) {
            //Debug.Log("you won!");
            Camewa.Blur(true);
            nextButton.SetActive(true);
            winText.SetActive(true);
            ignore = true;
        } else if (!ignore && (pm.failed || (!instantFeedback && pm.moveCnt >= 15))) {
            //Debug.Log("ah shit you dead noob");
            Camewa.Blur(true);
            retryButton.SetActive(true);
            lossText.SetActive(true);
            pm.failed = true;
            ignore = true;
        }
    }

    // adds circles, scales them and assignes them a color
    private void AddCircle(int x, int y) {
        Vector3 circlePos = new Vector3(coordPosX[y], coordPosY[x], 0);
        circle.GetComponent<SpriteRenderer>().sprite = objectList[0];
        GameObject gObj = Instantiate(circle, circlePos, Quaternion.identity) as GameObject;
        circleList.Add(gObj);
        Transform t = gObj.transform;
        t.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        
        gObj.GetComponent<SpriteRenderer>().material.color = c.colors[gameAreaColors[x, y]];
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
            pm.UndoLastMove();
        }
    }

    public void LevelLoader() {
        foreach (GameObject g in circleList) {
            Destroy(g);
        }

        circleList.Clear();

        nextButton.SetActive(false);
        winText.SetActive(false);
        retryButton.SetActive(false);
        lossText.SetActive(false);

        level = Config.getLevelNr();
        gameArea = Config.getGameArea();
        gameAreaColors = Config.getGameAreaColors();
        allowedMoves = Config.getAllowedMoves();
        ignore = false;

        pm.InitializeSpaceship();

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
        } else if (instantFeedback && Config.getLevelNr() == 1) {
            Destroy(confirmButton.gameObject);
            Destroy(undoBUtton.gameObject);
        }

        Camewa.Blur(false);

        levelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + level.ToString();
        TaskDescription.sprite = Resources.Load<Sprite>("Sprites/Level" + level.ToString());

    }
    public void nextLevel(){
        GatherData.stopLevel("Level " + level.ToString());
        Config.incrementLevelNr();
        
        if (Config.getWasFinalLevel()) {
            SceneManager.LoadScene("MainMenu");
        } else {
            LevelLoader();
        }
        GatherData.startLevel();
    }
    public void retryLevel(){
        GatherData.addFailure();
        LevelLoader();
    }
}