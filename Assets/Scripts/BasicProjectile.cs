using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileSprite;

    [SerializeField]
    private float movementSpeed = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Setup(float spriteZRotation, Vector3 headingDirection)
    {
        rb.velocity = headingDirection * movementSpeed;

        projectileSprite.transform.Rotate(Vector3.forward, spriteZRotation - 90f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
