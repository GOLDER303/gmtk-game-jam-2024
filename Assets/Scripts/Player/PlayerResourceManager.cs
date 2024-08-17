using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] scaleCountersTexts;

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

        ResourceType pickedResourceType = pickedRecourse.ResourceType;

        ownedResourcesCount[pickedResourceType]++;

        scaleCountersTexts[(int)pickedResourceType].text = ownedResourcesCount[pickedResourceType]
            .ToString();

        Destroy(other.gameObject);
    }
}
