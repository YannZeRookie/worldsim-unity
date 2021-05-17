using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class KPIDisplayer : AttributeDisplayer
{
    IWorld World; //TODO remove
    readonly Dictionary<IKpi, Text> Kpis = new Dictionary<IKpi, Text>();

    public void Initialize(IWorld world)
    {
        World = world;

        foreach (IKpi kpi in world.Kpis)
            Kpis.Add(kpi, CreateAttribute("", title: kpi.Name, permanent: true));
    }

    public void UpdateDisplay()
    {
        ResetAttributes();
        Positionner.RestartPosition();
        foreach (KeyValuePair<IKpi, Text> pair in Kpis)
            SetText(pair.Value, pair.Key.GetValue(World).ToString(), title: pair.Key.Name);
    }
}
