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
    WorldMap WorldMap;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    public void Initialize(WorldMap worldMap, IWorld world)
    {
        Time = world.Time;
        WorldMap = worldMap;

        Slider.maxValue = Time.LastIteration();
        Slider.value = Time.Iteration;
    }

    public void UpdateDate()
    {
        int Steps = Mathf.RoundToInt(Slider.value);

        Time.Iteration = Steps;
        WorldMap.UpdateDisplay();
    }
}
