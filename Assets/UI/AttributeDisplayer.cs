using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

[RequireComponent(typeof(DisplayerPositionner))]
public class AttributeDisplayer : MonoBehaviour
{
    [SerializeField]
    Text AttributePrefab;
    [SerializeField]
    Text BlockAttributePrefab;

    protected readonly List<Text> Attributes = new List<Text>();
    protected readonly List<Text> PermanentAttributes = new List<Text>();

    protected DisplayerPositionner Positionner;

    protected virtual void Awake()
    {
        Positionner = GetComponent<DisplayerPositionner>();
    }

    protected void DisplayAttributeContent(IDataNode dataNode, int indent = 0, string title = null)
    {
        if (dataNode is DataList list)
        {
            if (title != null)
                CreateAttribute("", indent, title);
            DisplayAttributeContent(list.First(), indent + 1);
            foreach (IDataNode obj in list.Skip(1))
            {
                Positionner.NextPosition();
                DisplayAttributeContent(obj, indent + 1);
            }
        }

        else if (dataNode is DataDictionary dictionary)
        {
            if (title != null)
                CreateAttribute("", indent, title);
            foreach (var pair in dictionary)
                DisplayAttributeContent(pair.Value, indent + 1, title = pair.Key);
        }

        else if (dataNode is DataValue value)
            CreateAttribute(value.StringValue(), indent, title);
        else
            Debug.LogError("Unknown type : " + dataNode.GetType());
    }

    protected Text CreateAttribute(string text, int indent = 0, string title = null, bool permanent = false)
    {
        Text DefaultAttribute = Instantiate(AttributePrefab, transform);

        Vector3 position = Positionner.NextPosition(RollBackEnabled: !permanent);
        DefaultAttribute.rectTransform.localPosition = position;

        SetText(DefaultAttribute, text, indent, title);

        if (permanent)
            PermanentAttributes.Add(DefaultAttribute);
        else
            Attributes.Add(DefaultAttribute);

        return DefaultAttribute;
    }

    protected List<Text> CreateBlocks(int number, bool permanent = false)
    {
        var blocks = new List<Text>();

        foreach (Vector3 position in Positionner.CreateColumns(number, !permanent))
        {
            Text block = Instantiate(BlockAttributePrefab, transform);
            block.rectTransform.localPosition = position;
            blocks.Add(block);
            if (permanent)
                PermanentAttributes.Add(block);
            else
                Attributes.Add(block);
        }

        return blocks;
    }

    protected void SetText(Text Attribute, string text, int indent = 0, string title = null)
    {
        Attribute.text = new string('\t', indent);

        if (title != null)
            Attribute.text += title + ": ";

        Attribute.text += text;
    }
    protected void ResetAttributes()
    {
        foreach (Text text in Attributes)
            Destroy(text.gameObject);

        Attributes.Clear();
    }
}
