using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class ResourceDisplayerManager : MonoBehaviour
{
    [SerializeField]
    ResourceDisplayer ResourceDisplayerPrefab;

    readonly List<ResourceDisplayer> ResourceDisplayers = new List<ResourceDisplayer>();

    public void CreateNewResource(IResource resource)
    {
        ResourceDisplayer ResourceDisplayer = Instantiate(ResourceDisplayerPrefab, transform);
        ResourceDisplayers.Add(ResourceDisplayer);

        Vector3 position = GetComponent<CellInfosDisplayerPositionner>().GetPosition(false);
        ResourceDisplayer.Initialize(resource, position);
    }

    public void UpdateDisplay(ICell cell)
    {
        foreach (ResourceDisplayer resourceDisplayer in ResourceDisplayers)
            resourceDisplayer.UpdateDisplay(cell);
    }
}
