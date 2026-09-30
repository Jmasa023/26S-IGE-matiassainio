using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerInput playerInput;
    public float movementSpeed = 5f;

    private Vector3 moveInput;

    public void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        moveInput = new Vector3(input.x, 0f, input.y);
    }

    public void OnBomb()
    {
        Debug.Log("Bomb dropped");
    }

    public void Update()
    {
        if (moveInput != Vector3.zero)
        {
            transform.forward = moveInput;
        }

        characterController.Move(moveInput * movementSpeed * Time.deltaTime);
    }
}