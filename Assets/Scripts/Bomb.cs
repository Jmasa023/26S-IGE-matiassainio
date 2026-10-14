using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Collider bombCollider;
    public float explosionTime;
    public int explosionRadius;

    private float elapsedTime;
    private Collider ownerCollider;
    private bool isIgnoringOwnerCollider = true;

    public void SetOwner(GameObject gameObject)
    {
        ownerCollider = gameObject.GetComponent<Collider>();
        Physics.IgnoreCollision(ownerCollider, bombCollider, true);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= explosionTime)
        {
            Explode();
        }

        if (isIgnoringOwnerCollider && !ownerCollider.bounds.Intersects(bombCollider.bounds))
        {
            Physics.IgnoreCollision(ownerCollider, bombCollider, false);
            isIgnoringOwnerCollider = false;
        }
    }

    private void Explode()
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        Vector3 center = transform.position + Vector3.up;
        float maxDistance = 2 * (explosionRadius - 1);
        RaycastHit hit;

        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.BoxCast(center, Vector3.one * 0.25f, directions[i], out hit, Quaternion.identity, maxDistance))
            {
            if (hit.collider.CompareTag("Destructible"))
            {
                Destroy(hit.collider.gameObject);
            }
            }
        }

        Destroy(gameObject);
    }
}