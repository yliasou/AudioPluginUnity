/// <summary>
/// MusicManager handles background music and sound effects (SFX) playback,
/// volume control, music fading, and queue management for a Unity project.
/// It supports playing music by index, fading between tracks, playing SFX by key or clip,
/// and managing a queue of music tracks. Singleton pattern is used for global access.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheFlow.Audio
{
    [System.Serializable]
    public class SFXEntry
    {
        /// <summary>
        /// Key for identifying the SFX.
        /// </summary>
        public string key;
        /// <summary>
        /// AudioClip associated with the key.
        /// </summary>
        public AudioClip clip;
    }

    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

        [Header("Audio Sources")]
        public AudioSource musicSource;
        public AudioSource sfxSource;

        [Header("Music List")]
        public List<AudioClip> musicList = new List<AudioClip>();

        [Header("SFX List")]
        public List<SFXEntry> sfxClips = new List<SFXEntry>();
        private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

        [Header("Fade Settings")]
        public float fadeDuration = 1.5f;

        private Coroutine fadeCoroutine;
        private Queue<int> musicQueue = new Queue<int>();
        private int currentMusicIndex = 0;

        /// <summary>
        /// Initializes the singleton instance and loads SFX entries into a dictionary.
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

            // Load SFX into dictionary
            foreach (var entry in sfxClips)
            {
                if (!sfxDictionary.ContainsKey(entry.key) && entry.clip != null)
                {
                    sfxDictionary.Add(entry.key, entry.clip);
                }
            }
        }

        /// <summary>
        /// Subscribes to settings events and initializes audio volumes and music.
        /// </summary>
        private void Start()
        {
            var settings = SettingsManager.Instance;

            settings.OnMusicVolumeChanged.AddListener(SetMusicVolume);
            settings.OnSFXVolumeChanged.AddListener(SetSFXVolume);
            settings.OnMusicChangeRequested.AddListener(PlayMusicByIndex);

            SetMusicVolume(settings.MusicVolume);
            SetSFXVolume(settings.SFXVolume);

            PlayMusicByIndex(settings.CurrentMusicIndex);
        }

        /// <summary>
        /// Sets the volume of the music audio source.
        /// </summary>
        /// <param name="volume">Volume value (0-1).</param>
        public void SetMusicVolume(float volume)
        {
            if (musicSource != null)
                musicSource.volume = volume;
        }

        /// <summary>
        /// Sets the volume of the SFX audio source.
        /// </summary>
        /// <param name="volume">Volume value (0-1).</param>
        public void SetSFXVolume(float volume)
        {
            if (sfxSource != null)
                sfxSource.volume = volume;
        }

        /// <summary>
        /// Plays a sound effect using the provided AudioClip.
        /// </summary>
        /// <param name="clip">AudioClip to play.</param>
        public void PlaySFX(AudioClip clip)
        {
            if (clip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        /// <summary>
        /// Plays a sound effect by its key.
        /// </summary>
        /// <param name="key">Key of the SFX to play.</param>
        public void PlaySFX(string key)
        {
            if (sfxDictionary.TryGetValue(key, out AudioClip clip))
            {
                PlaySFX(clip);
            }
            else
            {
                Debug.LogWarning($"[MusicManager] SFX key '{key}' not found.");
            }
        }

        /// <summary>
        /// Plays music from the music list by index, fading out the current track and fading in the new one.
        /// </summary>
        /// <param name="index">Index of the music track to play.</param>
        public void PlayMusicByIndex(int index)
        {
            if (musicList == null || musicList.Count == 0) return;

            index = Mathf.Clamp(index, 0, musicList.Count - 1);
            currentMusicIndex = index;

            AudioClip newClip = musicList[index];

            if (musicSource.clip != newClip)
            {
                if (fadeCoroutine != null)
                    StopCoroutine(fadeCoroutine);

                fadeCoroutine = StartCoroutine(FadeToMusic(newClip));
            }
        }

        /// <summary>
        /// Coroutine to fade out the current music, switch to a new clip, and fade in.
        /// </summary>
        /// <param name="newClip">The new AudioClip to play.</param>
        private IEnumerator FadeToMusic(AudioClip newClip)
        {
            float startVolume = musicSource.volume;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
                yield return null;
            }

            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(0f, SettingsManager.Instance.MusicVolume, t / fadeDuration);
                yield return null;
            }

            musicSource.volume = SettingsManager.Instance.MusicVolume;
            fadeCoroutine = null;
        }

        // ========== QUEUE SYSTEM ==========

        /// <summary>
        /// Adds a music track index to the queue.
        /// </summary>
        /// <param name="index">Index of the music track to queue.</param>
        public void QueueMusic(int index)
        {
            if (index >= 0 && index < musicList.Count)
                musicQueue.Enqueue(index);
        }

        /// <summary>
        /// Plays the next music track in the queue.
        /// </summary>
        public void PlayNextInQueue()
        {
            if (musicQueue.Count > 0)
            {
                int nextIndex = musicQueue.Dequeue();
                PlayMusicByIndex(nextIndex);
            }
        }

        /// <summary>
        /// Queues all music tracks except the currently playing one.
        /// </summary>
        public void QueueAllExceptCurrent()
        {
            musicQueue.Clear();
            for (int i = 0; i < musicList.Count; i++)
            {
                if (i != currentMusicIndex)
                    musicQueue.Enqueue(i);
            }
        }

        /// <summary>
        /// Clears the music queue.
        /// </summary>
        public void ClearQueue()
        {
            musicQueue.Clear();
        }
    }
}
