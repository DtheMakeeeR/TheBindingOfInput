using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    [Header("UI Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MasterParam = "MasterVol";
    private const string MusicParam = "MusicVol";
    private const string SfxParam = "SFXVol";

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(v => SetVolume(MasterParam, v));
        musicSlider.onValueChanged.AddListener(v => SetVolume(MusicParam, v));
        sfxSlider.onValueChanged.AddListener(v => SetVolume(SfxParam, v));
    }

    public void SetVolume(string parameterName, float value)
    {
        float clamped = Mathf.Clamp(value, 0.0001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f;
        mainMixer.SetFloat(parameterName, dB);
    }
}