using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DataManager;

public class HighscoreUIManager : MonoBehaviour
{
    public Text[] nicknameTexts = new Text[5];
    public Text[] scoreTexts = new Text[5];

    private void Start()
    {
        if (DataManager.Instance == null)
        {
            return;
        }

        DataManager.Instance.SortHighscores();

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            HighscoreData data = DataManager.Instance.highscores[i];
            if (data.points == 0)
            {
                continue;
            }

            nicknameTexts[i].text = data.name;
            scoreTexts[i].text = data.points.ToString();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
