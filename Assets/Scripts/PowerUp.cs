using UnityEngine;

public enum PowerUpType
{
    FirePack,
    BombPack,
    SpeedPack
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float rotationSpeed;
    public Transform visualRoot;
    public GameObject gameOverWindow;

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
        Destroy(gameObject);
    }
}
}