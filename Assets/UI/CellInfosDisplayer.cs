using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class CellInfosDisplayer : AttributeDisplayer
{
    [SerializeField]
    Text Type;
    [SerializeField]
    Text Efficiency;

    readonly Dictionary<IResource, BlockAttribute> Resources = new Dictionary<IResource, BlockAttribute>();

    public void Initialize(IWorld world)
    {
        var worldResources = new List<IResource>();
        foreach (var pair in world.Resources)
            worldResources.Add(pair.Value);

        var resourceBlocks = CreateResourceList(worldResources, permanent: true);

        for (int i = 0; i < worldResources.Count; i++)
            Resources.Add(worldResources[i], resourceBlocks[i]);

        SetCell(null);
    }

    public void SetCell(ICell cell)
    {

        if (cell != null)
        {
            SetText(Type, cell.Jm2.Id, title: "Type");
            if (cell.Jm2.Efficiency.HasValue)
                SetText(Efficiency, (cell.Jm2.Efficiency.Value * 100) + "%", title: "Efficiency");
            else
                SetText(Efficiency, "N/A", title: "Efficiency");

            foreach (var pair in Resources)
                SetBlockText(pair.Value, cell.GetStock(pair.Key.Id).ToString(), title: pair.Key.Name);

            DisplayJM2SpecificInfos(cell.Jm2);
        }
        else
        {
            SetText(Type, "", title: "Type");
            SetText(Efficiency, "", title: "Efficiency");

            foreach (var pair in Resources)
                SetBlockText(pair.Value, "", title: pair.Key.Name);

            ResetAttributes();
        }
    }

    private void DisplayJM2SpecificInfos(IJM2 jM2)
    {
        ResetAttributes();
        Positionner.RestartPosition();

        var tags = new HashSet<string>();

        foreach (var pair in jM2.Values)
            tags.Add(pair.Key);

        //Rich man's display
        ConsumeTags(tags, jM2.Values);

        //Poor man's display
        foreach (string key in tags)
            DisplayAttributeContent(jM2.Values[key], title: key);
    }

    private void ConsumeTags(HashSet<string> tags, DataDictionary values)
    {
        TryConsumeOpexOutput(tags, values);
    }

    private void TryConsumeOpexOutput(HashSet<string> tags, DataDictionary values)
    {
        if (!tags.Contains("output") || !tags.Contains("opex"))
            return;

        tags.Remove("output");
        tags.Remove("opex");

        var output = (DataDictionary)values["output"];
        var opex = (DataDictionary)values["opex"];

        var resourcesOutput = new List<string>();
        var resourcesOpex = new List<string>();

        foreach (var pair in output)
            resourcesOutput.Add(pair.Key);
        foreach (var pair in opex)
            resourcesOpex.Add(pair.Key);

        CreateAttribute("", title: "Production");
        var blocksOpex = CreateResourceList(resourcesOpex);

        for (int i = 0; i < blocksOpex.Count; i++)
            blocksOpex[i].Text.text = resourcesOpex[i] + "\n" + opex[resourcesOpex[i]].FloatValue();


        CreateBlocks(1)[0].Text.text = "||\n\\/";

        var blocksOutput = CreateResourceList(resourcesOutput);

        for (int i = 0; i < blocksOutput.Count; i++)
            blocksOutput[i].Text.text = resourcesOutput[i] + "\n" + output[resourcesOutput[i]].FloatValue();
    }
}
