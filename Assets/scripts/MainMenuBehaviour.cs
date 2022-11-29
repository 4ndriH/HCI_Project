using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void LoadInstant(){
        Config.setInstantFeedback(true);
        SceneManager.LoadScene("LevelPrototype");
    }
     public void LoadAfter(){
        Config.setInstantFeedback(false);
        SceneManager.LoadScene("LevelPrototype");
    }
}
