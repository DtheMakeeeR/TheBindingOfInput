using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FadePanelTrigger : MonoBehaviour
{
    [SerializeField] private FadePanel panel;
    [SerializeField] private bool showOnce = true;

    private bool _shown;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (showOnce && _shown) return;

        _shown = true;
        panel.Show();
    }
}