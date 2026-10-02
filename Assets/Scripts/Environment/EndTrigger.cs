using System.Collections;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject endCanvas;

    [Header("End Image")]
    [SerializeField] private CanvasGroup endImage;

    [Header("Fade")]
    [SerializeField] private float fadeDuration = 1.5f;

    [SerializeField] ScenesManager scenesManager;

    private bool _triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered) return;
        if (!other.CompareTag("Player")) return;

        _triggered = true;
        StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        mainCanvas.SetActive(false);
        endCanvas.SetActive(true);

        endImage.alpha = 0f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            endImage.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        endImage.alpha = 1f;
        yield return new WaitForSeconds(1);
        scenesManager.LoadScene(0);
    }
}