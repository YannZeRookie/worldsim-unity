using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSim.API;

public class CellConverter : MonoBehaviour
{
    [SerializeField]
    Sprite Factory;

    [SerializeField]
    Sprite Source;
    
    [SerializeField]
    Sprite Sink;

    public Sprite CellToSprite(ICell cell)
    {
        switch(cell.Jm2.Id)
        {
            case "factory":
                return Factory;
            case "source":
                return Source;
            case "sink":
                return Sink;
            default:
                throw new System.Exception("Unknown ID: " + cell.Jm2.Id);
        }
    }
}
