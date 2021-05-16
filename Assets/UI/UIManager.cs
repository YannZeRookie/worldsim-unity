using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldSim.API;

public class UIManager : MonoBehaviour
{
    public CellInfosDisplayer CellInfosDisplayer;
    public TimeSlider TimeSlider;

    public void Initialize(WorldMap worldMap, IWorld world)
    {
        foreach (IResource resource in world.Resources.Values)
            CellInfosDisplayer.ResourceDisplayerManager.CreateNewResource(resource);

        TimeSlider.Initialize(worldMap, world);
    }
}
