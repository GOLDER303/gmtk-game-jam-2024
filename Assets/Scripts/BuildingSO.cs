using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "BuildingSO")]
public class BuildingSO : ScriptableObject
{
    public Dictionary<ResourceType, int> ownedResourcesCount;
    public int redScalePrice;
    public int blueScalePrice;
    public int blackScalePrice;
    public Sprite sprite;
    public GameObject buildingPrefab;
}
