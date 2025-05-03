using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void Start()
    {
        DeadManager.Instance.OnPlayerDead += DeadManager_OnPlayerDead;
        Hide();
    }

    private void DeadManager_OnPlayerDead(object sender, System.EventArgs e)
    {
        Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        DeadManager.Instance.OnPlayerDead -= DeadManager_OnPlayerDead;
    }
}
