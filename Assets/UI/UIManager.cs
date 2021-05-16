using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldSim.API;

public class UIManager : MonoBehaviour
{
    public CellInfosDisplayer CellInfosDisplayer;

    public void Initialize(IWorld world)
    {
        foreach (IResource resource in world.Resources.Values)
            CellInfosDisplayer.ResourceDisplayerManager.CreateNewResource(resource);
    }
}
