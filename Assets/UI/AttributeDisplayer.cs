using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DisplayerPositionner))]
public class AttributeDisplayer : MonoBehaviour
{
    [SerializeField]
    Text DefaultAttributePrefab;

    protected readonly List<Text> Attributes = new List<Text>();
    protected readonly List<Text> PermanentAttributes = new List<Text>();

    protected DisplayerPositionner Positionner;

    protected virtual void Awake()
    {
        Positionner = GetComponent<DisplayerPositionner>();
    }

    protected void DisplayAttributeContent(object Attribute, int indent = 0, string title = null)
    {
        if (Attribute is List<object> list)
        {
            if (title != null)
                CreateAttribute("", indent, title);
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
            CreateAttribute(str, indent, title);
        else
            Debug.LogError("Unknown type : " + Attribute.GetType());
    }

    protected Text CreateAttribute(string text, int indent = 0, string title = null, bool permanent = false)
    {
        Text DefaultAttribute = Instantiate(DefaultAttributePrefab, transform);

        Vector3 position = Positionner.GetPosition(RollBackEnabled: !permanent);
        DefaultAttribute.rectTransform.localPosition = position;

        SetText(DefaultAttribute, text, indent, title);

        if (permanent)
            PermanentAttributes.Add(DefaultAttribute);
        else
            Attributes.Add(DefaultAttribute);

        return DefaultAttribute;
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
