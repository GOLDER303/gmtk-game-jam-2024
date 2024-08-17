using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileSprite;

    public void Setup(float spriteZRotation)
    {
        projectileSprite.transform.Rotate(Vector3.forward, spriteZRotation - 90f);
    }
}
