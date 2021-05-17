using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class CellInfosDisplayer : AttributeDisplayer
{
    [SerializeField]
    Text Type;
    [SerializeField]
    Text Efficiency;

    readonly Dictionary<IResource, Text> Resources = new Dictionary<IResource, Text>();

    public void Initialize(IWorld world)
    {
        foreach (KeyValuePair<string, IResource> pair in world.Resources)
            Resources.Add(pair.Value, CreateAttribute("", title: pair.Value.Name, permanent: true));
    }

    public void SetCell(ICell cell)
    {

        if (cell != null)
        {
            SetText(Type, cell.Jm2.Id, title: "Type");
            if (cell.Jm2.Efficiency.HasValue)
                SetText(Efficiency, (cell.Jm2.Efficiency.Value * 100) + "%", title: "Efficiency");
            else
                SetText(Efficiency, "N/A", title: "Efficiency");

            foreach (KeyValuePair<IResource, Text> pair in Resources)
                SetText(pair.Value, cell.GetStock(pair.Key.Id).ToString(), title: pair.Key.Name);

            DisplayJM2SpecificInfos(cell.Jm2);
        }
        else
        {
            SetText(Type, "", title: "Type");
            SetText(Efficiency, "", title: "Efficiency");

            foreach (KeyValuePair<IResource, Text> pair in Resources)
                SetText(pair.Value, "", title: pair.Key.Name);

            ResetAttributes();
        }
    }

    private void DisplayJM2SpecificInfos(IJM2 jM2)
    {
        ResetAttributes();
        Positionner.RestartPosition();

        foreach(KeyValuePair<string, object> pair in jM2.Init)
            DisplayAttributeContent(pair.Value, title: pair.Key);
    }
}
