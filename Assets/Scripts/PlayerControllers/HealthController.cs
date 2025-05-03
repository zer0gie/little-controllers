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
        }
        else
        {
            Debug.Log(_playerHealth + " HP, player dead");
            DeadManager.Instance.ConfirmPlayerDead();
        }

    }
}
