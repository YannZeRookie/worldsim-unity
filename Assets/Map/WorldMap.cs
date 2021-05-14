using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using WorldSim.API;

//Make the link between a Tilemap and an IWorld map
public class WorldMap : MonoBehaviour
{
    [SerializeField]
    CellDisplayer CellDisplayerPrefab;

    IWorld World;
    // Start is called before the first frame update

    public Tilemap Tilemap { get; private set; }
    public CellConverter TileConverter { get; private set; }

    void Start()
    {
        World = GetComponent<BootWorldSim>().World;
        TileConverter = GetComponent<CellConverter>();
        Tilemap = GetComponentInChildren<Tilemap>();

        foreach(ICell cell in World.Map.Cells)
        {
            CellDisplayer CDisplayer = Instantiate(CellDisplayerPrefab, transform);
            CDisplayer.Initialize(this, cell);
        }
    }
}
