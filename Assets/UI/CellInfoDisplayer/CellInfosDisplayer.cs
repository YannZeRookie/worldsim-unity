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

    public ResourceDisplayerManager ResourceDisplayerManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ResourceDisplayerManager = GetComponent<ResourceDisplayerManager>();
    }

    public void SetCell(ICell cell)
    {
        Type.text = "Type: ";
        Efficiency.text = "Efficiency: ";

        if(cell != null)
        {
            Type.text += cell.Jm2.Id;
            if (cell.Jm2.Efficiency.HasValue)
                Efficiency.text += (cell.Jm2.Efficiency.Value * 100) + "%";
            else
                Efficiency.text += "N/A";
        }

        ResourceDisplayerManager.UpdateDisplay(cell);
        if (cell != null)
            DisplayJM2SpecificInfos(cell.Jm2);
        else
            ResetAttributes();
    }

    private void DisplayJM2SpecificInfos(IJM2 jM2)
    {
        ResetAttributes();
        Positionner.RestartPosition();

        foreach(KeyValuePair<string, object> pair in jM2.Init)
            DisplayAttributeContent(pair.Value, title: pair.Key);
    }
}
