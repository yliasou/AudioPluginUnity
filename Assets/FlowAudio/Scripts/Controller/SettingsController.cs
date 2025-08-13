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
    public Button playMusicButton;
    public Button playSfxButton;
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
        playMusicButton.onClick.AddListener(OnPlayMusicButtonClicked);
        playSfxButton.onClick.AddListener(OnPlaySfxButtonClicked);
    }

    public void OnCloseButtonClicked()
    {
        SettingsLoader.Instance.CloseSettings();
    }
    public void OnPlayMusicButtonClicked()
    {
        float newValue = musicSlider.value > 0 ? 0f : 1f;
        musicSlider.value = newValue;
        SettingsManager.Instance.SetMusicVolume(newValue);
    }
    public void OnPlaySfxButtonClicked()
    {
        float newValue = sfxSlider.value > 0 ? 0f : 1f;
        sfxSlider.value = newValue;
        SettingsManager.Instance.SetSFXVolume(newValue);
    }
}
