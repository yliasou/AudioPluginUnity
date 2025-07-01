using TheFlow.Audio;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SettingsController manages the UI for audio settings, including music and SFX volume sliders,
/// and music track navigation buttons. It synchronizes UI elements with the SettingsManager.
/// </summary>
public class SettingsController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Music Control Buttons")]
    public Button prevMusicButton;
    public Button nextMusicButton;
    [Header("Exit Button")]
    public Button exitButton;

    /// <summary>
    /// Initializes the UI sliders and buttons, and sets up listeners to update settings when UI changes.
    /// </summary>
    private void Start()
    {
        musicSlider.value = SettingsManager.Instance.MusicVolume;
        sfxSlider.value = SettingsManager.Instance.SFXVolume;

        musicSlider.onValueChanged.AddListener(SettingsManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SettingsManager.Instance.SetSFXVolume);
        prevMusicButton.onClick.AddListener(() => SettingsManager.Instance.ChangeMusic(-1));
        nextMusicButton.onClick.AddListener(() => SettingsManager.Instance.ChangeMusic(1));
        exitButton.onClick.AddListener(OnCloseButtonClicked);
    }

    public void OnCloseButtonClicked()
    {
        SettingsLoader.Instance.CloseSettings();
    }
}
