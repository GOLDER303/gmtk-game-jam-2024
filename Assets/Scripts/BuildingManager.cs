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
    private AudioSource errorAudioSource;

    private void Start()
    {
        errorAudioSource = GetComponent<AudioSource>();
    }

    public void BuildBuilding(int buildingId)
    {
        if (
            playerResourceManager.GetResourceCount(ResourceType.RedScale)
                >= buildingSOs[buildingId].redScalePrice
            && playerResourceManager.GetResourceCount(ResourceType.Gold)
                >= buildingSOs[buildingId].blueScalePrice
        )
        {
            currentBuildingSO = buildingSOs[buildingId];

            playerResourceManager.ChangeResourceCount(
                ResourceType.RedScale,
                -currentBuildingSO.redScalePrice
            );
            playerResourceManager.ChangeResourceCount(
                ResourceType.Gold,
                -currentBuildingSO.blueScalePrice
            );

            buildingPlacementIndicator.SetSprite(currentBuildingSO.sprite);
            buildingPlacementIndicator.Activate();
        }
        else
        {
            errorAudioSource.Play();
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
