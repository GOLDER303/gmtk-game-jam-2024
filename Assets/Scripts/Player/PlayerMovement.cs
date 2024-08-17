using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    private Vector2 moveVector;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        Vector3 direction = new Vector3(moveVector.x, -1f, moveVector.y).normalized;

        if (direction.magnitude >= .1f)
        {
            rb.velocity = speed * direction;
        }
    }
}
