using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using WorldSim.API;

//Make the link between a Tilemap and an IWorld map
public class WorldMap : MonoBehaviour
{
    IWorld World;
    // Start is called before the first frame update

    Tilemap Tilemap;
    TileConverter TileConverter;

    void Start()
    {
        World = GetComponent<BootWorldSim>().World;
        TileConverter = GetComponent<TileConverter>();
        Tilemap = GetComponentInChildren<Tilemap>();

        foreach(ICell cell in World.Map.Cells)
        {
            Tile tile = TileConverter.CellToTile(cell);

            Tilemap.SetTile(new Vector3Int(cell.X, cell.Y, 0), tile);
        }
    }
}
