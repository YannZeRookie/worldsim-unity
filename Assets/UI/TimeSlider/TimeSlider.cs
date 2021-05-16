using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class TimeSlider : MonoBehaviour
{
    Slider Slider;
    ITime Time;
    IWorld World;
    WorldMap WorldMap;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    public void Initialize(WorldMap worldMap, IWorld world)
    {
        Time = world.Time;
        WorldMap = worldMap;

        switch (Time.StepUnit)
        {
            case TimeStep.day:
                Slider.maxValue = (Time.End - Time.Start).Days -1;
                break;
            case TimeStep.month:
                Slider.maxValue = 12 * (Time.End.Year - Time.Start.Year) + Time.End.Month - Time.Start.Month -1;
                break;
            case TimeStep.year:
                Slider.maxValue = Time.End.Year - Time.Start.Year -1;
                break;
        }

        Slider.SetValueWithoutNotify(Time.StepValue);
        UpdateDate();
    }

    public void UpdateDate()
    {
        int Steps = Mathf.RoundToInt(Slider.value);

        Time.Iteration = Steps;
        WorldMap.UpdateDisplay();
    }
}
