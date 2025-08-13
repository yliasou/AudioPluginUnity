/// <summary>
/// SettingsManager handles the storage, retrieval, and broadcasting of audio settings,
/// including music volume, SFX volume, and the current music track index. It uses UnityEvents
/// to notify listeners of changes and persists settings using PlayerPrefs. Implements a singleton pattern.
/// </summary>
using UnityEngine;
using UnityEngine.Events;

namespace TheFlow.Audio
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;

        /// <summary>
        /// Gets the current music volume (0-1).
        /// </summary>
        public float MusicVolume { get; private set; }
        /// <summary>
        /// Gets the current SFX volume (0-1).
        /// </summary>
        public float SFXVolume { get; private set; }
        /// <summary>
        /// Gets the index of the currently selected music track.
        /// </summary>
        public int CurrentMusicIndex { get; private set; }

        /// <summary>
        /// Event invoked when the music volume changes.
        /// </summary>
        public UnityEvent<float> OnMusicVolumeChanged = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when the SFX volume changes.
        /// </summary>
        public UnityEvent<float> OnSFXVolumeChanged = new UnityEvent<float>();
        /// <summary>
        /// Event invoked when a music track change is requested.
        /// </summary>
        public UnityEvent<int> OnMusicChangeRequested = new UnityEvent<int>();

        /// <summary>
        /// Initializes the singleton instance and loads saved settings.
        /// </summary>
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }

        /// <summary>
        /// Sets the music volume, saves it, and notifies listeners.
        /// </summary>
        /// <param name="volume">Volume value (0-1).</param>
        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            OnMusicVolumeChanged.Invoke(volume);
        }

        /// <summary>
        /// Sets the SFX volume, saves it, and notifies listeners.
        /// </summary>
        /// <param name="volume">Volume value (0-1).</param>
        public void SetSFXVolume(float volume)
        {
            SFXVolume = volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);
            OnSFXVolumeChanged.Invoke(volume);
        }

        /// <summary>
        /// Changes the current music track index by the given direction, saves it, and notifies listeners.
        /// </summary>
        /// <param name="direction">Direction to change the music index (e.g., -1 for previous, 1 for next).</param>
        public void ChangeMusic(int direction)
        {
            CurrentMusicIndex += direction;
            if (CurrentMusicIndex < 0) CurrentMusicIndex = 0;

            PlayerPrefs.SetInt("CurrentMusicIndex", CurrentMusicIndex);
            OnMusicChangeRequested.Invoke(CurrentMusicIndex);
        }

        /// <summary>
        /// Loads saved settings for music volume, SFX volume, and current music index from PlayerPrefs.
        /// </summary>
        private void LoadSettings()
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            CurrentMusicIndex = PlayerPrefs.GetInt("CurrentMusicIndex", 0);
        }
    }
}
