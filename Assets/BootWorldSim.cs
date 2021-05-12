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
    private void Awake()
    {
        Engine engine = new Engine();
        engine.LoadYaml(FileName);
        IWorld world = engine.World;
        string type = world.Type;

        TypeDisplay.text = type;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
