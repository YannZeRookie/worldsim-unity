using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class ResourceDisplayer : MonoBehaviour
{
    Text Text;
    IResource resource;

    void Awake()
    {
        Text = GetComponent<Text>();
    }

    public void Initialize(IResource resource, Vector3 localPosition)
    {
        this.resource = resource;
        UpdateDisplay(null);
        transform.localPosition = localPosition;
    }

    public void UpdateDisplay(ICell cell)
    {
        Text.text = resource.Name + ": ";

        if (cell != null)
            Text.text += cell.GetStock(resource.Id);
    }
}
