using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using WorldSim.API;

//Make the link between a Tilemap and an IWorld map
public class WorldMap : MonoBehaviour
{
    public UIManager UIManager;

    IWorld World;
    // Start is called before the first frame update

    public Tilemap Tilemap { get; private set; }
    public CellConverter CellConverter { get; private set; }

    void Start()
    {
        World = GetComponent<BootWorldSim>().World;
        CellConverter = GetComponent<CellConverter>();
        Tilemap = GetComponentInChildren<Tilemap>();
        UIManager = FindObjectOfType<UIManager>();

        foreach(ICell cell in World.Map.Cells)
        {
            CellDisplayer CellDisplayerPrefab = CellConverter.CellToDisplayer(cell);
            CellDisplayer CDisplayer = Instantiate(CellDisplayerPrefab, transform);
            CDisplayer.Initialize(this, cell);
        }
    }
}
