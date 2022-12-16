using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject nameInput;

    // Loads the instant feedback version
    public void LoadInstant()
    {
        Config.setInstantFeedback(true);
        Config.resetLevelNr();
        Debug.Log(nameInput.GetComponent<TMP_InputField>().text);
        Config.setName(nameInput.GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("LevelPrototype");
    }

    // Loads the version where you submit at the end
    public void LoadAfter()
    {
        Config.setInstantFeedback(false);
        Config.resetLevelNr();
        Config.setName(nameInput.GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("LevelPrototype");
    }
}
