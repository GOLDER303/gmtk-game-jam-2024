using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    private BasicProjectile basicProjectilePrefab;

    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    private Transform projectileContainer;

    [SerializeField]
    private LayerMask mousePositionRayLayerMask;

    private AudioSource shootEffectAudioSource;

    private void Start()
    {
        shootEffectAudioSource = GetComponent<AudioSource>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        shootEffectAudioSource.Play();

        Vector3 mousePosition = Mouse.current.position.ReadValue();

        BasicProjectile newProjectile = Instantiate(
            basicProjectilePrefab,
            shootingPoint.position,
            Quaternion.Euler(Constants.CameraXRotation, 0, 0)
        );

        Vector2 playerCharacterScreenPosition = Camera.main.WorldToScreenPoint(
            shootingPoint.position
        );

        float projectileRotation = Vector2.SignedAngle(
            Vector2.right,
            (Vector2)mousePosition - playerCharacterScreenPosition
        );

        Vector3 mouseWorldPosition = Vector3.zero;

        Ray cameraRay = Camera.main.ScreenPointToRay(mousePosition);

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

        Vector3 projectileHeadingDirection = (
            mouseWorldPosition - shootingPoint.position
        ).normalized;

        projectileHeadingDirection.y = 0;

        newProjectile.Setup(projectileRotation, projectileHeadingDirection);
    }
}
