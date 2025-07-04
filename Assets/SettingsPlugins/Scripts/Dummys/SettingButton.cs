using TheFlow.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenPopUp);
    }

    public void OpenPopUp()
    {
        SettingsLoader.Instance.OpenSettings();
    }
}
