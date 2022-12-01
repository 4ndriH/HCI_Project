using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject nameInput;

    public void LoadInstant(){
        Config.setInstantFeedback(true);
        Config.resetLevelNr();
        Debug.Log(nameInput.GetComponent<TMP_InputField>().text);
        Config.setName(nameInput.GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("LevelPrototype");
    }
     public void LoadAfter(){
        Config.setInstantFeedback(false);
        Config.resetLevelNr();
        Config.setName(nameInput.GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("LevelPrototype");
    }
}
