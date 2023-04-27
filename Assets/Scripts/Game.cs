using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    //public static int OpenLevels { get;private set; } = 1;

    private static int _levelOne = 2;
    public static int CurrentLevel { get; private set; } = 1;

    //public static void LevelOpen()
    //{
    //    OpenLevels++;
    //}

    public static void LevelUp() 
    {
        CurrentLevel++;
    }

    public static void LevelOne()
    {
        CurrentLevel = _levelOne;
    }

    //public static void LevelDown()
    //{
    //    CurrentLevel--;
    //}
}
