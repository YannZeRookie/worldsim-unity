using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class CellInfosDisplayer : MonoBehaviour
{
    [SerializeField]
    Text Type;
    [SerializeField]
    Text Efficiency;
    [SerializeField]
    Text DefaultAttributePrefab;

    readonly List<Text> Attributes = new List<Text>();

    public ResourceDisplayerManager ResourceDisplayerManager { get; private set; }
    CellInfosDisplayerPositionner Positionner;

    private void Awake()
    {
        ResourceDisplayerManager = GetComponent<ResourceDisplayerManager>();
        Positionner = GetComponent<CellInfosDisplayerPositionner>();
    }

    public void SetCell(ICell cell)
    {
        Type.text = "Type: ";
        Efficiency.text = "Efficiency: ";

        if(cell != null)
        {
            Type.text += cell.Jm2.Id;
            if (cell.Jm2.Efficiency.HasValue)
                Efficiency.text += (cell.Jm2.Efficiency.Value * 100) + "%";
            else
                Efficiency.text += "N/A";
        }

        ResourceDisplayerManager.UpdateDisplay(cell);
        if (cell != null)
            DisplayJM2SpecificInfos(cell.Jm2);
        else
            ResetAttributes();
    }

    private void DisplayJM2SpecificInfos(IJM2 jM2)
    {
        ResetAttributes();
        Positionner.RestartPosition();

        foreach(KeyValuePair<string, object> pair in jM2.Init)
            DisplayAttributeContent(pair.Value, title: pair.Key);
    }

    private void DisplayAttributeContent(object Attribute, int indent = 0, string? title = null)
    {
        if (Attribute is List<object> list)
        {
            if (title != null)
                CreateAttribute(title + ":", indent);
            DisplayAttributeContent(list.First(), indent + 1);
            foreach (object obj in list.Skip(1))
            {
                Positionner.GetPosition();
                DisplayAttributeContent(obj, indent + 1);
            }
        }

        else if (Attribute is Dictionary<object, object> dictionnary)
            foreach (KeyValuePair<object, object> pair in dictionnary)
                DisplayAttributeContent(pair.Value, indent + 1, title = (string)pair.Key);

        else if (Attribute is string str)
        {
            if (title != null)
                CreateAttribute(title + ": " + str, indent);
            else
                CreateAttribute(str, indent);
        }
        else
            Debug.LogError("Unknown type : " + Attribute.GetType());
    }

    private void CreateAttribute(string text, int indent = 0)
    {
        Text DefaultAttribute = Instantiate(DefaultAttributePrefab, transform);

        Vector3 position = Positionner.GetPosition();
        DefaultAttribute.rectTransform.localPosition = position;
        DefaultAttribute.text = new string('\t', indent) + text;

        Attributes.Add(DefaultAttribute);
    }

    private void ResetAttributes()
    {
        foreach(Text text in Attributes)
            Destroy(text.gameObject);

        Attributes.Clear();
    }
}
