using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSim.API;

public class CellDisplayer : MonoBehaviour
{
    Tilemap Tilemap;
    ICell Cell;
    UIManager UIManager;

    public void Initialize(WorldMap worldMap, ICell Cell)
    {
        this.Cell = Cell;
        Tilemap = worldMap.Tilemap;
        UIManager = worldMap.UIManager;

        //Sprite
        GetComponent<SpriteRenderer>().sprite = worldMap.TileConverter.CellToSprite(Cell);

        //Position
        Vector3Int gridPostition = new Vector3Int(Cell.X, Cell.Y, 0);
        transform.position = Tilemap.CellToWorld(gridPostition);
    }

    private void OnMouseEnter()
    {
        UIManager.JM2Displayer.SetJM2(Cell.Jm2);
    }
}
