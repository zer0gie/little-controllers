using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadManager : MonoBehaviour
{
    public event EventHandler OnPlayerDead;

    public static DeadManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ConfirmPlayerDead()
    {
        OnPlayerDead?.Invoke(this, EventArgs.Empty);
        StartCoroutine(GameRestart());
    }

    private IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}