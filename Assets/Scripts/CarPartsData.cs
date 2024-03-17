using System;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsData : MonoBehaviour
{
    public List<CarPartData> Parts = new();
}

[Serializable]
public class CarPartData
{
    public string partName;
    public Transform viewPoint;
    public Material material;
    public bool canBeMetallic;
}