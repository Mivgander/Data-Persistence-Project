using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public ColorPicker ballColorPicker;
    public ColorPicker paddleColorPicker;
    public Toggle hardModeToggle;

    private void Start()
    {
        ballColorPicker.Init();
        ballColorPicker.onColorChanged += SetBallColor;
        ballColorPicker.SelectColor(SettingsManager.Instance.ballColor);

        paddleColorPicker.Init();
        paddleColorPicker.onColorChanged += SetPaddleColor;
        paddleColorPicker.SelectColor(SettingsManager.Instance.paddleColor);

        hardModeToggle.onValueChanged.AddListener(SetHardMode);
    }

    public void SetBallColor(Color color)
    {
        SettingsManager.Instance.ballColor = color;
    }

    public void SetPaddleColor(Color color)
    {
        SettingsManager.Instance.paddleColor = color;
    }

    public void SetHardMode(bool hardMode)
    {
        SettingsManager.Instance.hardMode = hardMode;
    }

    public void MainMenu()
    {
        SettingsManager.Instance.SaveSettings();
        SceneManager.LoadScene(0);
    }
}
