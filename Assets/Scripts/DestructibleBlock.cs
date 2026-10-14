using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BlockDestroyed();
        }
    }
}