using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class CellInfosDisplayer : MonoBehaviour
{
    [SerializeField]
    Text Type;
    [SerializeField]
    Text Efficiency;

    public ResourceDisplayerManager ResourceDisplayerManager { get; private set; }

    private void Awake()
    {
        ResourceDisplayerManager = GetComponent<ResourceDisplayerManager>();
    }

    public void SetCell(ICell cell)
    {
        Type.text = "Type: ";
        Efficiency.text = "Efficiency: ";

        ResourceDisplayerManager.UpdateDisplay(cell);

        if(cell != null)
        {
            Type.text += cell.Jm2.Id;
            if (cell.Jm2.Efficiency.HasValue)
                Efficiency.text += (cell.Jm2.Efficiency.Value * 100) + "%";
            else
                Efficiency.text += "N/A";
        }
    }
}
