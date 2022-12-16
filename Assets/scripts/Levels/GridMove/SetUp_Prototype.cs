using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SetUp_Prototype : MonoBehaviour
{
    private bool instantFeedback = Config.getInstantFeedback();
    private bool ignore = false;
    
    private Colors c = new Colors();
    public Image TaskDescription;
    public PlayerMovement pm;
    private int level;
    
    public List<Sprite> objectList = new List<Sprite>();
    
    public Button quitButton;
    public Button confirmButton;
    public Button undoBUtton;
    
    public GameObject circle;
    public GameObject nextButton;
    public GameObject retryButton;
    
    public GameObject levelText;
    public GameObject winText;
    public GameObject lossText;
    public GameObject moveCounter;

    private List<GameObject> circleList = new List<GameObject>();

    public int[,] gameArea;
    public int[,] gameAreaColors;
    public List<(int x, int y, bool goal)> allowedMoves;

    // define the center points of the tiles and where the character moves to
    private float[] coordPosX = new float[5] { -0.5f, 1.25f, 3f, 4.75f, 6.5f };
    private float[] coordPosY = new float[5] { 3.5f, 1.75f, 0f, -1.75f, -3.5f };


    // Start is called before the first frame update
    void Start()
    {
        LevelLoader();
        quitButton.onClick.AddListener(() => quit());

        GatherData.startLevel();
    }

    // Checks if the level has been completed or the player has failed
    void Update()
    {
        moveCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Moves left: " + (15 - pm.moveCnt).ToString();
        if (pm.success && !ignore)
        { 
            Camewa.Blur(true);
            nextButton.SetActive(true);
            winText.SetActive(true);
            ignore = true;
        }
        else if (!ignore && (pm.failed || (!instantFeedback && pm.moveCnt >= 15)))
        {
            Camewa.Blur(true);
            retryButton.SetActive(true);
            lossText.SetActive(true);
            pm.failed = true;
            ignore = true;
        }
        else if (pm.instantFeedbackRestart)
        {
            LevelLoader();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (nextButton.activeSelf == true)
            {
                nextLevel();
            }
            else if (retryButton.activeSelf == true)
            {
                retryLevel();
            }
        }
    }

    // Add, scale and color circles
    private void AddCircle(int x, int y)
    {
        Vector3 circlePos = new Vector3(coordPosX[y], coordPosY[x], 0);
        circle.GetComponent<SpriteRenderer>().sprite = objectList[0];
        GameObject gObj = Instantiate(circle, circlePos, Quaternion.identity) as GameObject;
        circleList.Add(gObj);
        Transform t = gObj.transform;
        t.localScale = new Vector3(0.55f, 0.55f, 0.55f);

        gObj.GetComponent<SpriteRenderer>().material.color = c.colors[gameAreaColors[x, y]];
    }

    // Button to submit ones solution
    // verifies that the performed moves match the correct ones
    private void buttonClickSubmit()
    {
        GatherData.addSubmit();

        if (pm.moveTracker.Count != allowedMoves.Count)
        {
            pm.failed = true;
            return;
        }

        for (int i = 0; i < pm.moveTracker.Count; i++)
        {
            if (allowedMoves[i].x != pm.moveTracker[i].x || allowedMoves[i].y != pm.moveTracker[i].y)
            {
                pm.failed = true;
                return;
            }
        }

        pm.success = true;
    }

    // Button to go back one move
    private void buttonClickUndo()
    {
        GatherData.addUndo();
        if (pm.moveCnt > 0)
        {
            // comment
            pm.UndoLastMove();
        }
    }

    // Loads the level data from the config and sets up the level
    public void LevelLoader()
    {
        foreach (GameObject g in circleList)
        {
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

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (gameArea[x, y] != 0)
                {
                    AddCircle(x, y);
                }
            }
        }
        quitButton.onClick.AddListener(() => quit());

        if (!instantFeedback && Config.getLevelNr() == 1)
        {
            confirmButton.onClick.AddListener(() => buttonClickSubmit());
            undoBUtton.onClick.AddListener(() => buttonClickUndo());
        }
        else if (instantFeedback && Config.getLevelNr() == 1 && !pm.instantFeedbackRestart)
        {
            Destroy(confirmButton.gameObject);
            Destroy(undoBUtton.gameObject);
        }

        Camewa.Blur(false);

        levelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + level.ToString();
        TaskDescription.sprite = Resources.Load<Sprite>("Sprites/Level" + level.ToString());
    }

    // Function for the next level button, writes data to log and initiates load of the next level
    public void nextLevel()
    {
        GatherData.stopLevel("Level " + level.ToString());
        Config.incrementLevelNr();

        if (Config.getWasFinalLevel())
        {
            GatherData.writeLogToFile();

            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            LevelLoader();
        }
        GatherData.startLevel();
    }

    // Function for the retry level button, restarts the level and increments error counter
    public void retryLevel()
    {
        GatherData.addFailure();
        LevelLoader();
    }

    // Function for the quit button, writes data to log file and returns to the variant menu
    public void quit()
    {
        GatherData.stopLevel("Level " + level.ToString());
        Config.incrementLevelNr();
        GatherData.writeLogToFile();

        SceneManager.LoadScene("MainMenu");
    }
}