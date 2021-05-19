using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class ResourceSpriteLister : MonoBehaviour
{
    [SerializeField]
    private List<BlockAttribute> ResourceBlocks = new List<BlockAttribute>();

    public BlockAttribute GetResourceBlock(IResource resource)
    {
        return GetResourceBlock(resource.Id);
    }
    public BlockAttribute GetResourceBlock(string resourceID)
    {
        foreach (BlockAttribute block in ResourceBlocks)
            if (block.gameObject.name.Equals(resourceID))
                return block;

        Debug.LogError("Uknown resource: " + resourceID);
        return null;
    }
}
