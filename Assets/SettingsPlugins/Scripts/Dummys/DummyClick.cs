using TheFlow.Audio;
using UnityEngine;

public class DummyClick : MonoBehaviour
{
    /// <summary>
    /// Simulates a click event to open the settings.
    /// </summary>
    public void OnClick()
    {
        MusicManager.Instance.PlaySFX("Click"); 
    }

}