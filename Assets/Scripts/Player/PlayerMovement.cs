using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private SpriteRenderer playerSprite;

    private Vector2 moveVector;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        playerAnimator.SetFloat("Speed", Mathf.Abs(speed * moveVector.magnitude));

        Vector3 direction = new Vector3(moveVector.x, 0f, moveVector.y).normalized;

        rb.velocity = speed * direction;

        if (direction.x > 0)
        {
            playerSprite.flipX = true;
        }
        else if (direction.x < 0)
        {
            playerSprite.flipX = false;
        }
    }
}
