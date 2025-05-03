using UnityEngine;

public class HastePickup : MonoBehaviour
{
    public HastePickupConfig hastePickupConfig;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out MovementController movementController))
        {
            movementController.HastePickup(hastePickupConfig.speedBoost);
            Destroy(gameObject);
        }
    }
}