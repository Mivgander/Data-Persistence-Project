using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public HighscoreData[] highscores = new HighscoreData[5];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighscores();
    }

    public class SaveData
    {
        public HighscoreData[] highscores = new HighscoreData[5];
    }

    [System.Serializable]
    public class HighscoreData
    {
        public string name;
        public int points;
    }

    public void SaveHighscore(int points)
    {
        int lowestPointsHighscoreIndex = 0;

        for (int i = 0; i < highscores.Length; i++)
        {
            if (highscores[lowestPointsHighscoreIndex].points > highscores[i].points)
            {
                lowestPointsHighscoreIndex = i;
            }
        }

        if (highscores[lowestPointsHighscoreIndex].points > points)
        {
            return;
        }

        HighscoreData data = new HighscoreData();
        data.name = playerName;
        data.points = points;
        highscores[lowestPointsHighscoreIndex] = data;

        SaveData saveData = new SaveData();
        saveData.highscores = highscores;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscores = data.highscores;
        }

        SortHighscores();
    }

    public HighscoreData GetHighestScore()
    {
        HighscoreData playerHigestScore = new HighscoreData();
        playerHigestScore.name = playerName;
        playerHigestScore.points = 0;

        for (int i = 0; i < highscores.Length; i++)
        {
            if (highscores[i].name == "")
            {
                continue;
            }

            if (playerHigestScore.points < highscores[i].points)
            {
                playerHigestScore = highscores[i];
            }
        }

        return playerHigestScore;
    }

    public void SortHighscores()
    {
        Array.Sort(highscores, delegate(HighscoreData x, HighscoreData y) { return x.points < y.points ? 1 : -1; });
    }
}
