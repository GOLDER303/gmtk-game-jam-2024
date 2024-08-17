using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    private readonly Dictionary<ResourceType, int> ownedResourcesCount = new();

    private void Start()
    {
        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            ownedResourcesCount.Add(resourceType, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Resource"))
        {
            return;
        }

        PickableResource pickedRecourse = other.gameObject.GetComponent<PickableResource>();

        ownedResourcesCount[pickedRecourse.ResourceType]++;

        Destroy(other.gameObject);
    }
}
