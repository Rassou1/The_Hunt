using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioAssetPack", menuName = "SoundEffectFolder")]
public class Sounds : ScriptableObject
{
    [SerializeField]
    public AudioClip[] sounds;
}
