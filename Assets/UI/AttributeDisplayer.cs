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
    BlockAttribute BlockAttributePrefab;
    [SerializeField]
    ResourceSpriteLister ResourceList;

    protected readonly List<GameObject> Attributes = new List<GameObject>();
    protected readonly List<GameObject> PermanentAttributes = new List<GameObject>();
        
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
            CreateAttribute(value.StringValue, indent, title);
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
            PermanentAttributes.Add(DefaultAttribute.gameObject);
        else
            Attributes.Add(DefaultAttribute.gameObject);

        return DefaultAttribute;
    }

    private List<BlockAttribute> CreateBlocks(List<BlockAttribute> blocks, bool permanent)
    {
        var Createdblocks = new List<BlockAttribute>();
        var positions = Positionner.CreateColumns(blocks.Count, RollBackEnabled: !permanent);

        for (int i = 0; i < blocks.Count; i++)
        {
            BlockAttribute block = Instantiate(blocks[i], transform);
            block.transform.localPosition = positions[i];
            Createdblocks.Add(block);
            if (permanent)
                PermanentAttributes.Add(block.gameObject);
            else
                Attributes.Add(block.gameObject);
        }

        return Createdblocks;
    }

    protected List<BlockAttribute> CreateBlocks(int number, bool permanent = false)
    {
        List<BlockAttribute> blocks = new List<BlockAttribute>();

        for (int i = 0; i < number; i++)
            blocks.Add(BlockAttributePrefab);

        return CreateBlocks(blocks, permanent);
    }

    protected List<BlockAttribute> CreateResourceList(List<string> resourceIDs, bool permanent = false)
    {
        List<BlockAttribute> blocks = new List<BlockAttribute>();

        foreach (var resource in resourceIDs)
            blocks.Add(ResourceList.GetResourceBlock(resource));

        return CreateBlocks(blocks, permanent);
    }
    protected List<BlockAttribute> CreateResourceList(List<IResource> resources, bool permanent = false)
    {
        List<BlockAttribute> blocks = new List<BlockAttribute>();

        foreach (var resource in resources)
            blocks.Add(ResourceList.GetResourceBlock(resource));

        return CreateBlocks(blocks, permanent);
    }

    protected void SetText(Text Attribute, string text, int indent = 0, string title = null)
    {
        Attribute.text = new string('\t', indent);

        if (title != null)
            Attribute.text += title + ": ";

        Attribute.text += text;
    }
    protected void SetBlockText(BlockAttribute block, string text, string title = null)
    {
        block.Text.text = "";

        if (title != null)
            block.Text.text = title + "\n";

        block.Text.text += text;
    }
    protected void ResetAttributes()
    {
        foreach (GameObject gameObject in Attributes)
            Destroy(gameObject);

        Attributes.Clear();
    }
}
