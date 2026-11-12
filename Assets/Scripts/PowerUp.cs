using UnityEngine;

public enum PowerUpType
{
    FirePack,
    BombPack,
    SpeedPack,
    HealthPack
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float rotationSpeed;
    public Transform visualRoot;
    public GameObject gameOverWindow;
    private bool pickedUp = false;

    private void Update()
    {
        visualRoot.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
    PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.UpgradePowers(powerUpType);
            AudioManager.Instance.PlayPowerUpPickUpSfx();
            pickedUp = true;
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
    if (!pickedUp && GameManager.Instance != null && GameManager.Instance.isGameRunning)
    {
        AudioManager.Instance.PlayPowerUpDestroyedSfx();
    }
    }
}