using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Тайминги")]
    [SerializeField] private float defaultDuration = 3f;

    [Header("Печатная машинка")]
    [SerializeField] private float charDelay = 0.03f;
    [SerializeField] private float punctuationPause = 0.4f;
    [SerializeField] private float spacePause = 0.05f;
    [SerializeField] private string punctuationChars = ".!?…";
    [Header("Фразы при смерти")]
    [SerializeField]
    private string[] deathPhrases;

    private readonly Queue<(string text, float duration)> _queue = new();
    private Typewriter _typewriter;
    private Coroutine _routine;
    private bool _isShowing;
    private int _lastDeathPhraseIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _typewriter = new Typewriter(
            messageText,
            charDelay,
            punctuationPause,
            spacePause,
            punctuationChars
        );

        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    public void ShowMessage(string text, float duration = -1f)
    {
        if (messagePanel == null || messageText == null) return;

        _queue.Enqueue((text, duration > 0 ? duration : defaultDuration));

        if (!_isShowing)
            _routine = StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        _isShowing = true;

        while (_queue.Count > 0)
        {
            var (text, duration) = _queue.Dequeue();

            messagePanel.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(
                messagePanel.GetComponent<RectTransform>()
            );

            yield return StartCoroutine(_typewriter.Type(text));

            yield return new WaitForSeconds(duration);

            messagePanel.SetActive(false);
        }

        _isShowing = false;
        _routine = null;
    }
    public void ShowRandomDeathPhrase(float duration = -1f)
    {
        if (deathPhrases == null || deathPhrases.Length == 0) return;

        int index = GetRandomIndexAvoidingRepeat(deathPhrases.Length, _lastDeathPhraseIndex);
        _lastDeathPhraseIndex = index;

        ShowMessage(deathPhrases[index], duration);
    }

    private int GetRandomIndexAvoidingRepeat(int length, int lastIndex)
    {
        if (length <= 1) return 0;

        int index;
        do
        {
            index = Random.Range(0, length);
        }
        while (index == lastIndex);

        return index;
    }

    private class Typewriter
    {
        private readonly TextMeshProUGUI _text;
        private readonly float _charDelay;
        private readonly float _punctuationPause;
        private readonly float _spacePause;
        private readonly string _punctuationChars;

        public Typewriter(
            TextMeshProUGUI text,
            float charDelay,
            float punctuationPause,
            float spacePause,
            string punctuationChars)
        {
            _text = text;
            _charDelay = charDelay;
            _punctuationPause = punctuationPause;
            _spacePause = spacePause;
            _punctuationChars = punctuationChars;
        }

        public IEnumerator Type(string fullText)
        {
            _text.text = fullText;
            _text.maxVisibleCharacters = 0;

            for (int i = 0; i < fullText.Length; i++)
            {
                _text.maxVisibleCharacters = i + 1;

                char c = fullText[i];
                float delay = _charDelay;

                if (_punctuationChars.IndexOf(c) >= 0)
                    delay += _punctuationPause;
                else if (c == ' ')
                    delay += _spacePause;

                yield return new WaitForSeconds(delay);
            }
        }
    }
}