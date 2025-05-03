using UnityEngine;

public class DamagePickup : MonoBehaviour
{
    public DamagePickupConfig damagePickupConfig;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out HealthController healthController))
        {
            healthController.DamagePickup(damagePickupConfig.damage);
            Destroy(gameObject);
        }
    }
}
