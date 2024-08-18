using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private BuildingPlacementIndicator buildingPlacementIndicator;

    [SerializeField]
    private PlayerResourceManager playerResourceManager;

    [SerializeField]
    private BuildingSO[] buildingSOs;

    private BuildingSO currentBuildingSO;

    public void BuildBuilding(int buildingId)
    {
        if (
            playerResourceManager.GetResourceCount(ResourceType.RedScale)
                >= buildingSOs[buildingId].redScalePrice
            && playerResourceManager.GetResourceCount(ResourceType.BlueScale)
                >= buildingSOs[buildingId].blueScalePrice
            && playerResourceManager.GetResourceCount(ResourceType.BlackScale)
                >= buildingSOs[buildingId].blackScalePrice
        )
        {
            currentBuildingSO = buildingSOs[buildingId];

            playerResourceManager.ChangeResourceCount(
                ResourceType.RedScale,
                -currentBuildingSO.redScalePrice
            );
            playerResourceManager.ChangeResourceCount(
                ResourceType.BlueScale,
                -currentBuildingSO.blueScalePrice
            );
            playerResourceManager.ChangeResourceCount(
                ResourceType.BlackScale,
                -currentBuildingSO.blackScalePrice
            );

            buildingPlacementIndicator.SetSprite(currentBuildingSO.sprite);
            buildingPlacementIndicator.Activate();
        }
        else
        {
            // TODO
            Debug.Log("You can't afford this building!");
        }
    }

    public void OnPlaceBuilding()
    {
        if (!currentBuildingSO)
        {
            return;
        }

        Vector3 newBuildingPosition = buildingPlacementIndicator.transform.position;
        buildingPlacementIndicator.Deactivate();

        GameObject buildingPrefab = currentBuildingSO.buildingPrefab;

        Instantiate(
            buildingPrefab,
            newBuildingPosition,
            Quaternion.Euler(Constants.CameraXRotation, 0f, 0f)
        );

        currentBuildingSO = null;
    }
}
