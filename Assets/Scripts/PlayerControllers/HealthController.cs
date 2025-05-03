using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int _playerHealth;
    
    private void Start()
    {
        _playerHealth = 100;
    }

    public void DamagePickup(int damage)
    {
        _playerHealth -= damage;

        if (_playerHealth >= 0)
        {
            Debug.Log(_playerHealth + " HP");
            //UIManager.Instance.DamageUIActivate();
            return;
        }
        else
        {
            Debug.Log(_playerHealth + " HP, player dead");
            //UIManager.Instance.DeadUIActivate();
            //DeathManager.Instance.OnPlayerDead();
        }

    }
}
