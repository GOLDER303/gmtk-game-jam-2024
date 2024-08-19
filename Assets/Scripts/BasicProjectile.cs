using UnityEngine;

public class BasicProjectile : Projectile
{

    [SerializeField]
    private GameObject projectileSprite;

    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float maxLifeTimeInSeconds = 8f;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, maxLifeTimeInSeconds);
    }

    public void Setup(float spriteZRotation, Vector3 headingDirection)
    {
        rb.velocity = headingDirection * movementSpeed;

        projectileSprite.transform.Rotate(Vector3.forward, spriteZRotation - 90f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
