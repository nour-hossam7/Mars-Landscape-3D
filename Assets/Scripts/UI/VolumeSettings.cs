using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider volumeSlider;

    [Header("Settings")]
    [SerializeField, Range(0f, 1f)] private float defaultVolume = 0.75f;

    private const string VolumeKey = "MasterVolume";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(
            VolumeKey,
            defaultVolume
        );

        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.wholeNumbers = false;
            volumeSlider.SetValueWithoutNotify(savedVolume);

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp01(volume);

        AudioListener.volume = clampedVolume;

        PlayerPrefs.SetFloat(
            VolumeKey,
            clampedVolume
        );

        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }
}