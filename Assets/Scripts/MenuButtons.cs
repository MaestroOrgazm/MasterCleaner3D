using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private GameObject _tutorialPanel;

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetEventReload();
    }

    public void SkipTutorial()
    {
        _tutorialPanel.SetActive(false);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        SetEventMenu();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void OpenSetting()
    {
        _settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        _settingPanel.SetActive(false);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(Game.CurrentScene);
    }

    private void SetEventReload()
    {
        var properties = new Dictionary<string, object>()
        {
            {"level", Game.CurrentLevel},
            {"days_since_reg", GameStart._daysSinceReg}
        };
        AnalyticsEvent.SendReportEvent("level_restart", properties);
    }

    private void SetEventMenu()
    {
        var properties = new Dictionary<string, object>()
        {
            {"days_since_reg", GameStart._daysSinceReg}
        };
        AnalyticsEvent.SendReportEvent("main_menu", properties);
    }

    //public void LevelUp()
    //{
    //    if (Game.CurrentLevel < Game.OpenLevels)
    //    {
    //        Game.LevelUp();
    //        _currentLevelText.text = Game.CurrentLevel.ToString();
    //    }
    //}

    //public void LevelDown()
    //{
    //    if (Game.CurrentLevel > 1)
    //    { 
    //        Game.LevelDown();
    //        _currentLevelText.text = Game.CurrentLevel.ToString();
    //    }
    //}
}
