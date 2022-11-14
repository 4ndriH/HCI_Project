using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void LoadGrid(){
        SceneManager.LoadScene("Grid_test");
    }
     public void LoadLine(){
        SceneManager.LoadScene("Line_test");
    }
     public void LoadHarsh(){
        SceneManager.LoadScene("Harsh_failure");
    }
     public void LoadSoft(){
        SceneManager.LoadScene("Soft_failure");
    }
}
