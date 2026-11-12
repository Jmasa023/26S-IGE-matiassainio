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
    public int bombExplosionRadius = 1;
    public int maxActiveBombs = 1;
    public float movementSpeedMultiplier = 1f;
    private int currentActiveBombs = 0;

    public void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
            
        GameManager.Instance.RegisterPlayer(this);
        GameUI.Instance.SetPlayerHealthLabel(health);
    }
    public void TakeDamage()
    {
        health -= 1;
        AudioManager.Instance.PlayPlayerHitSfx();
        GameUI.Instance.SetPlayerHealthLabel(health);

        if (health <= 0)
        {
            playerInput.enabled = false;
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
        if (currentActiveBombs >= maxActiveBombs)
        {
            return;
        }
        Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject bombObject = Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        Bomb bomb = bombObject.GetComponent<Bomb>();
        bomb.Initialize(this, bombExplosionRadius);
        currentActiveBombs += 1;
        AudioManager.Instance.PlayBombPlaceSfx();
    }

    public void Update()
    {
        if (moveInput != Vector3.zero)
        {
            transform.forward = moveInput;
        }

        characterController.Move(moveInput * movementSpeed * movementSpeedMultiplier * Time.deltaTime);
    }
    public void UpgradePowers(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.FirePack:
                bombExplosionRadius += 1;
                break;
            case PowerUpType.BombPack:
                maxActiveBombs += 1;
                break;
            case PowerUpType.SpeedPack:
                movementSpeedMultiplier += 0.5f;
                break;
            case PowerUpType.HealthPack:
                health += 1;
                GameUI.Instance.SetPlayerHealthLabel(health);
                break;
        }
    }

    public void OnBombDestroyed()
    {
        currentActiveBombs -= 1;
    }
}