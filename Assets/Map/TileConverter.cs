using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSim.API;

public class TileConverter : MonoBehaviour
{
    [SerializeField]
    Tile FactoryTile;

    [SerializeField]
    Tile SourceTile;
    
    [SerializeField]
    Tile SinkTile;

    public Tile CellToTile(ICell cell)
    {
        switch(cell.Jm2.Id)
        {
            case "factory":
                return FactoryTile;
            case "source":
                return SourceTile;
            case "sink":
                return SinkTile;
            default:
                throw new System.Exception("Unknown ID: " + cell.Jm2.Id);
        }
    }
}
