using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerInput playerInput;
    public float movementSpeed = 5f;
    public GameObject bombPrefab;
    private Vector3 moveInput;
    public int health = 2;

    public void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    GameManager.Instance.RegisterPlayer(this);
    }
    public void TakeDamage()
    {
        health -= 1;

        if (health <= 0)
        {
            GameManager.Instance.HandlePlayerDeath();
        }
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        moveInput = new Vector3(input.x, 0f, input.y);
    }

    void OnBomb()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject bombObject = Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        Bomb bomb = bombObject.GetComponent<Bomb>();
        bomb.SetOwner(gameObject);
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