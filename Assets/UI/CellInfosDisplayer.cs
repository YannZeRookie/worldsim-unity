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

    readonly Dictionary<IResource, Text> Resources = new Dictionary<IResource, Text>();

    public void Initialize(IWorld world)
    {
        foreach (KeyValuePair<string, IResource> pair in world.Resources)
            Resources.Add(pair.Value, CreateAttribute("", title: pair.Value.Name, permanent: true));
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
                SetText(pair.Value, cell.GetStock(pair.Key.Id).ToString(), title: pair.Key.Name);

            DisplayJM2SpecificInfos(cell.Jm2);
        }
        else
        {
            SetText(Type, "", title: "Type");
            SetText(Efficiency, "", title: "Efficiency");

            foreach (var pair in Resources)
                SetText(pair.Value, "", title: pair.Key.Name);

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

        CreateAttribute("", title: "Production");
        {
            var blocks = CreateBlocks(opex.Count);

            int index = 0;
            foreach (var pair in opex)
            {
                blocks[index++].text = pair.Key + "\n" + pair.Value.FloatValue();
            }
        }
        

        CreateBlocks(1)[0].text = "||\n\\/";

        {
            var blocks = CreateBlocks(output.Count);

            int index = 0;
            foreach (var pair in output)
            {
                blocks[index++].text = pair.Key + "\n" + pair.Value.FloatValue();
            }
        }
    }
}
