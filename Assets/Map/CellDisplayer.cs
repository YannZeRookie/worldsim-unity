using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSim.API;

public class CellDisplayer : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer Ground;

    [SerializeField]
    public string ID;

    Tilemap Tilemap;
    ICell Cell;
    UIManager UIManager;

    public void Initialize(WorldMap worldMap, ICell Cell)
    {
        this.Cell = Cell;
        Tilemap = worldMap.Tilemap;
        UIManager = worldMap.UIManager;

        //Position
        Vector3Int gridPostition = new Vector3Int(Cell.X, Cell.Y, 0);
        transform.position = Tilemap.CellToWorld(gridPostition);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (Cell.Jm2.Efficiency.HasValue)
            Ground.color = Color.Lerp(Color.red, Color.clear, Cell.Jm2.Efficiency.Value);
        else
            Ground.color = Color.clear;
    }

    private void OnMouseEnter()
    {
        UIManager.CellInfosDisplayer.SetCell(Cell);
    }

    private void OnMouseExit()
    {
        UIManager.CellInfosDisplayer.SetCell(null);
    }
}
