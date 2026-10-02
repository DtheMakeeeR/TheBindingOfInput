using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    [Header("Сообщение")]
    [SerializeField] private string messageText = "Привет!";
    [SerializeField] private float duration = 3f;
    [SerializeField] private bool showOnce = true;

    private bool _shown;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (showOnce && _shown) return;

        _shown = true;
        MessageManager.Instance.ShowMessage(messageText, duration);
    }
}