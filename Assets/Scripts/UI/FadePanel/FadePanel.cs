using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadePanel : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float fadeInDuration = 0.4f;
    [SerializeField] private float visibleDuration = 5f;
    [SerializeField] private float fadeOutDuration = 0.4f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private CanvasGroup _canvasGroup;
    private Coroutine _routine;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (_routine != null)
            StopCoroutine(_routine);

        gameObject.SetActive(true);
        _routine = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        yield return Fade(0f, 1f, fadeInDuration);

        yield return new WaitForSeconds(visibleDuration);

        yield return Fade(1f, 0f, fadeOutDuration);

        gameObject.SetActive(false);
        _routine = null;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        if (duration <= 0f)
        {
            _canvasGroup.alpha = to;
            yield break;
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float curveValue = fadeCurve.Evaluate(t);
            _canvasGroup.alpha = Mathf.Lerp(from, to, curveValue);
            yield return null;
        }

        _canvasGroup.alpha = to;
    }
}