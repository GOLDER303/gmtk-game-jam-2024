using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float Damage
    {
        get => damage;
    }

    [SerializeField]
    private GameObject projectileSprite;

    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float maxLifeTimeInSeconds = 8f;

    [SerializeField]
    private float damage = 10f;

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
        Destroy(gameObject);
    }
}
