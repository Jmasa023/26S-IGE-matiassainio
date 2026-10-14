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
        int explosionDistance = 2 * explosionRadius;
        int mask = LayerMask.GetMask("Bombable", "Blocker");
        int blockerLayer = LayerMask.NameToLayer("Blocker");

        Collider[] overlaps = Physics.OverlapBox(center, Vector3.one * 0.25f, Quaternion.identity, mask);
        for (int i = 0; i < overlaps.Length; i++)
        {
            PlayerController player = overlaps[i].GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
                break;
            }
        }

        RaycastHit hit;
        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.BoxCast(center, Vector3.one * 0.25f, directions[i], out hit, Quaternion.identity, explosionDistance, mask))
            {
                if (hit.collider.gameObject.layer == blockerLayer)
                {
                    continue;
                }

                PlayerController player = hit.collider.GetComponentInParent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage();
                }
                else
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        Destroy(gameObject);
    }
}