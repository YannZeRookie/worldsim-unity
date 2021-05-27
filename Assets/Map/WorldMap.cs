using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using WorldSim.API;

//Make the link between a Tilemap and an IWorld map
public class WorldMap : MonoBehaviour
{
    public UIManager UIManager { get; private set; }
    public SelectionManager SelectionManager { get; private set; }

    IWorld World;
    // Start is called before the first frame update

    public Tilemap Tilemap { get; private set; }
    public CellConverter CellConverter { get; private set; }

    readonly List<CellDisplayer> cellDisplayers = new List<CellDisplayer>();

    void Start()
    {
        World = GetComponent<BootWorldSim>().World;
        CellConverter = GetComponent<CellConverter>();
        Tilemap = GetComponentInChildren<Tilemap>();
        UIManager = FindObjectOfType<UIManager>();
        SelectionManager = GetComponent<SelectionManager>();

        UIManager.Initialize(this, World);

        foreach(ICell cell in World.Map.Cells)
        {
            CellDisplayer CellDisplayerPrefab = CellConverter.CellToDisplayer(cell);
            CellDisplayer CellDisplayer = Instantiate(CellDisplayerPrefab, transform);
            CellDisplayer.Initialize(this, cell);
            cellDisplayers.Add(CellDisplayer);
        }
    }

    public void UpdateDisplay()
    {
        UIManager.UpdateDisplay();
        foreach (CellDisplayer cell in cellDisplayers)
            cell.UpdateDisplay();
    }
}
