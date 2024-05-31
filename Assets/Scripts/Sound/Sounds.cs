using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioAssetPack", menuName = "SoundEffectFolder")]
public class Sounds : ScriptableObject
{
    //this script is a simple way to laod in a soundfile is just used for the diamonds but could be used for more-Jonathan
    [SerializeField]
    public AudioClip[] sounds;
}
