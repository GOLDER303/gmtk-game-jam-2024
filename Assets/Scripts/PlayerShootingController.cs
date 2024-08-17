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

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        BasicProjectile newProjectile = Instantiate(
            basicProjectilePrefab,
            shootingPoint.position,
            Quaternion.Euler(Constants.CameraXRotation, 0, 0)
        );

        Vector2 playerCharacterScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        float projectileRotation = Vector2.SignedAngle(
            Vector2.right,
            (Vector2)Input.mousePosition - playerCharacterScreenPosition
        );

        newProjectile.Setup(projectileRotation);
    }
}
