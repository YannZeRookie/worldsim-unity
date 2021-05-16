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

    [SerializeField]
    RectTransform StartPosition;
    [SerializeField]
    float Distance;

    public void CreateNewResource(IResource resource)
    {
        ResourceDisplayer ResourceDisplayer = Instantiate(ResourceDisplayerPrefab, transform);
        ResourceDisplayers.Add(ResourceDisplayer);
        ResourceDisplayer.Initialize(resource, StartPosition.localPosition);
        StartPosition.localPosition += Vector3.down * Distance;
    }

    public void UpdateDisplay(ICell cell)
    {
        foreach (ResourceDisplayer resourceDisplayer in ResourceDisplayers)
            resourceDisplayer.UpdateDisplay(cell);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(StartPosition.position, Vector3.right* 30);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(StartPosition.position + Vector3.down * Distance, Vector3.right * 30);
    }
}
