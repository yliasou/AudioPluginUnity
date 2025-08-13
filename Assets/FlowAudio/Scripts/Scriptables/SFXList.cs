using UnityEngine;
using System.Collections.Generic;
using TheFlow.Audio;

[CreateAssetMenu(menuName = "Audio/SFX List")]
public class SFXList : ScriptableObject
{
    public List<SFXEntry> sfxClips = new List<SFXEntry>();
}