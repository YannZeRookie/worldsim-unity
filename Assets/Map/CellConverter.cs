using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSim.API;

public class CellConverter : MonoBehaviour
{
    [SerializeField]
    CellDisplayer[] cellDisplayerPrefabs;

    public CellDisplayer CellToDisplayer(ICell cell)
    {
        foreach (CellDisplayer prefab in cellDisplayerPrefabs)
            if (prefab.ID.Equals(cell.Jm2.Id))
                return prefab;

        throw new System.Exception("Unknown ID: " + cell.Jm2.Id);
    }
}
