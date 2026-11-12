using UnityEngine;
using System.Collections.Generic;

public class PowerUpBlock : MonoBehaviour
{
    public List<PowerUp> powerUps;

    public void OnDestroy()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 1f, transform.position.z);

            int index = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[index], spawnPos, Quaternion.identity);

            GameManager.Instance.BlockDestroyed();
        }
    }
}