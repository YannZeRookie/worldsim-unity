using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSim.API;

public class CellDisplayer : MonoBehaviour
{
    Tilemap Tilemap;
    ICell Cell;

    public void Initialize(WorldMap worldMap, ICell Cell)
    {
        this.Cell = Cell;
        Tilemap = worldMap.Tilemap;

        //Sprite
        GetComponent<SpriteRenderer>().sprite = worldMap.TileConverter.CellToSprite(Cell);

        //Position
        Vector3Int gridPostition = new Vector3Int(Cell.X, Cell.Y, 0);
        transform.position = Tilemap.CellToWorld(gridPostition);
    }
}
