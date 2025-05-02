using System;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public event EventHandler OnDamagePickup;
    public event EventHandler OnNotePickup;
    public event EventHandler OnBoostPickup;
    public static PickupManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
