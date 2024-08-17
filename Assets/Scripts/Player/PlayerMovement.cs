using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    private Vector2 moveVector;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(moveVector.x, -1f, moveVector.y).normalized;

        if (direction.magnitude >= .1f)
        {
            characterController.Move(speed * Time.deltaTime * direction);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
}
