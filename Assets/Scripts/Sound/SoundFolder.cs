using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AudioAssetPack",menuName = "SoundTest")]
public class SoundFolder : ScriptableObject
{
    [SerializeField]
    public AudioClip[] walkClips;
    [SerializeField]
    public AudioClip[] runClips;
    [SerializeField]
    public AudioClip[] JumpStartClips;
    [SerializeField]
    public AudioClip[] JumpEndClips;
    [SerializeField]
    public AudioClip[] slideclips;
}
