using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldSim.API;
using WorldSim.Engine;
using UnityEngine.UI;

public class BootWorldSim : MonoBehaviour
{
    [SerializeField] private String FileName;

    [SerializeField] private Text TypeDisplay;
    // Awake is called before everything else
    public IWorld World { get; private set; }

    private void Awake()
    {
        Engine engine = new Engine();
        engine.LoadYaml(FileName);
        World = engine.World;
        string type = World.Type;

        TypeDisplay.text = type;
    }
}
