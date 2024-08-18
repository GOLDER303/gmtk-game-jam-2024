using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private BuildingPlacementIndicator buildingPlacementIndicator;

    [SerializeField]
    private Sprite[] buildingSprites;

    public void BuildBuilding(int buildingId)
    {
        buildingPlacementIndicator.SetSprite(buildingSprites[buildingId]);
        buildingPlacementIndicator.Activate();
    }

    public void OnPlaceBuilding()
    {
        buildingPlacementIndicator.Deactivate();
    }
}
