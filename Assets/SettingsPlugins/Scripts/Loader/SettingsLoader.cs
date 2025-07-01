using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SettingsLoader : MonoBehaviour
{
    [Header("Settings")]
    public string settingsSceneName = "Settings";

    private bool isSettingsSceneLoaded = false;
    public static SettingsLoader Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenSettings()
    {
        if (!isSettingsSceneLoaded)
        {
            SceneManager.LoadSceneAsync(settingsSceneName, LoadSceneMode.Additive)
                        .completed += OnSettingsSceneLoaded;

            isSettingsSceneLoaded = true;
        }
    }

    public void CloseSettings()
    {
        if (isSettingsSceneLoaded)
        {
            SceneManager.UnloadSceneAsync(settingsSceneName);
            isSettingsSceneLoaded = false;
        }
    }
    
    private void OnSettingsSceneLoaded(AsyncOperation op)
    {
        Scene settingsScene = SceneManager.GetSceneByName(settingsSceneName);

        if (settingsScene.isLoaded)
        {
            GameObject[] rootObjects = settingsScene.GetRootGameObjects();

            foreach (GameObject obj in rootObjects)
            {
                // Disable any AudioListener in the additive scene
                AudioListener listener = obj.GetComponentInChildren<AudioListener>(true);
                if (listener != null)
                {
                    listener.enabled = false;
                }
            }
        }
    }
}
