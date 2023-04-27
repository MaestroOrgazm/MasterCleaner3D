using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    [SerializeField] private Dirt[] _dirts;
    [SerializeField] private GameObject _losePannel;
    [SerializeField] private GameObject _winPannel;
    [SerializeField] private GameObject _oneStar;
    [SerializeField] private GameObject _twoStar;
    [SerializeField] private TMP_Text _text;

    private int _needEnerhyThree = 75;
    private int _needEnetgyTwo = 50;

    private void Awake()
    {
        Time.timeScale = 1;
        _robot.SetValues(_dirts.Length);
        _text.text = "Dirts gathered: 0/" + _dirts.Length;
        _robot._finishLevel += Finish;
        _robot._loseLevel += Lose;
        _robot._dirtCleaned += CurrentDirts;
    }

    private void OnDisable()
    {
        _robot._finishLevel -= Finish;
        _robot._loseLevel -= Lose;
        _robot._dirtCleaned -= CurrentDirts;
    }

    private void Finish()
    {
        Time.timeScale = 0;
        _winPannel.SetActive(true);

        if (_robot.Energy > _needEnerhyThree)
        {
            _oneStar.SetActive(true);
            _twoStar.SetActive(true);
        }
        else if (_robot.Energy > _needEnetgyTwo)
        {
            _oneStar.SetActive(true);
        }

        //if (SceneManager.GetActiveScene().buildIndex > Game.OpenLevels)
        //    Game.LevelOpen();

        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex+1) 
            Game.LevelUp();
        else
            Game.LevelOne();
    }

    private void Lose()
    {
        Time.timeScale = 0;
        _losePannel.SetActive(true);
    }

    private void CurrentDirts(int dirt)
    {
        _text.text = "Dirts gathered: " + dirt + "/" + _dirts.Length;
    }
}
