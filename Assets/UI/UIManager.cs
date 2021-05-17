using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldSim.API;

public class UIManager : MonoBehaviour
{
    public CellInfosDisplayer CellInfosDisplayer;
    public KPIDisplayer KPIDisplayer;
    public TimeSlider TimeSlider;

    public void Initialize(WorldMap worldMap, IWorld world)
    {
        CellInfosDisplayer.Initialize(world);
        KPIDisplayer.Initialize(world);
        TimeSlider.Initialize(worldMap, world);
    }

    public void UpdateDisplay() => KPIDisplayer.UpdateDisplay();
}
