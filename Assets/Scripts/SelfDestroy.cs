using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float lifetime = 5.0f;
    private float elapsed;

    public void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}