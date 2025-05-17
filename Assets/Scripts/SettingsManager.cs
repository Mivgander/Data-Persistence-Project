using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public bool hardMode = false;
    public Color ballColor = Color.gray;
    public Color paddleColor = Color.gray;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    [System.Serializable]
    public class SettingsData
    {
        public bool hardMode = false;
        public Color ballColor;
        public Color paddleColor;
    }

    public void SaveSettings()
    {
        SettingsData data = new SettingsData();
        data.hardMode = hardMode;
        data.ballColor = ballColor;
        data.paddleColor = paddleColor;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/settingsfile.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settingsfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);

            hardMode = data.hardMode;
            ballColor = data.ballColor;
            paddleColor = data.paddleColor;
        }
    }
}
