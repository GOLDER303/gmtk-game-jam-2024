using UnityEngine;

public class PickableResource : MonoBehaviour
{
    public ResourceType ResourceType
    {
        get => resourceType;
    }

    [SerializeField]
    private ResourceType resourceType;
}
