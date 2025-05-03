using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void Start()
    {
        DeadManager.Instance.OnPlayerDead += GameManager_OnPlayerDead;
        Hide();
    }

    private void GameManager_OnPlayerDead(object sender, System.EventArgs e)
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
        DeadManager.Instance.OnPlayerDead -= GameManager_OnPlayerDead;
    }
}
