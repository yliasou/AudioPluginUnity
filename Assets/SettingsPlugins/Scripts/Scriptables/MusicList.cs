using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Audio/Music List")]
public class MusicList : ScriptableObject
{
    public List<AudioClip> musicList = new List<AudioClip>();
}