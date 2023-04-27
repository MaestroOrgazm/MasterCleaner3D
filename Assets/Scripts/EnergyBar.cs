using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Robot _robot;

    private void Update()
    {
        _slider.value = _robot.Energy / 100;
    }
}
