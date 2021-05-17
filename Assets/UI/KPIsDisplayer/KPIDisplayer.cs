using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldSim.API;

public class KPIDisplayer : AttributeDisplayer
{
    IWorld World; //TODO remove
    List<IKpi> Kpis;

    public void Initialize(IWorld world)
    {
        Kpis = world.Kpis;
        World = world;
    }

    public void UpdateDisplay()
    {
        ResetAttributes();
        Positionner.RestartPosition();
        foreach (IKpi kpi in Kpis)
            DisplayKPI(kpi);
    }

    void DisplayKPI(IKpi kpi)
    {
        CreateAttribute(kpi.Name + ": " + kpi.GetValue(World));
    }
}
