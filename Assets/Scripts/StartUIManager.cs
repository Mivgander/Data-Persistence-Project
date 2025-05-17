using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartUIManager : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField playerNameField;

    private void Start()
    {
        startButton.interactable = false;
        playerNameField.onValueChanged.AddListener(OnNameChange);
        if (DataManager.Instance.playerName != "")
        {
            playerNameField.text = DataManager.Instance.playerName;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }

    public void OnNameChange(string newPlayerName)
    {
        DataManager.Instance.playerName = newPlayerName;
        if (newPlayerName == "")
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void HighscoreScene()
    {
        SceneManager.LoadScene(2);
    }

    public void SettingsScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
