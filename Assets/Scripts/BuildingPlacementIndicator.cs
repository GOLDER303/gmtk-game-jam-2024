using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPlacementIndicator : MonoBehaviour
{
    [SerializeField]
    private LayerMask mousePositionRayLayerMask;

    private bool isActive = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        transform.position = GetMouseWorldPosition();
    }

    public void Activate()
    {
        isActive = true;
        spriteRenderer.enabled = true;
    }

    public void Deactivate()
    {
        isActive = false;
        spriteRenderer.enabled = false;
    }

    public void SetSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = Vector3.zero;

        Ray cameraRay = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (
            Physics.Raycast(
                cameraRay,
                out RaycastHit hitInfo,
                float.MaxValue,
                mousePositionRayLayerMask
            )
        )
        {
            mouseWorldPosition = hitInfo.point;
        }

        return mouseWorldPosition;
    }
}
