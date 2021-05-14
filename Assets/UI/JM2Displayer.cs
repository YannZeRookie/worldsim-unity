using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldSim.API;

public class JM2Displayer : MonoBehaviour
{
    [SerializeField]
    Text ID;

    public void SetJM2(IJM2 JM2)
    {
        ID.text = "ID: " + JM2.Id;
    }
}
