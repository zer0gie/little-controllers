using UnityEngine;

public class MobileUI : MonoBehaviour
{
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
