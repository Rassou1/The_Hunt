using System.Collections;
using Alteruna;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : AttributesSync
{
    //This script plays out sounds where the avatar of another player is -Jonathan
    AudioSource _audioSoruce;
    Alteruna.Avatar _avatar;
    PlayerWalking _playerSounds;
    Vector3 oldPosition;
    void Start()
    {
        _avatar = GetComponentInParent<Alteruna.Avatar>();
        _audioSoruce = GetComponent<AudioSource>();
        _playerSounds = gameObject.GetComponentInParent<PlayerWalking>();
    }

    [SynchronizableMethod]
    public void NonLocalPlayerTest()
    {
        if (_avatar.IsMe)
            return;

        _audioSoruce.spatialBlend = 1.0f;

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(oldPosition.x, 0, oldPosition.z)) > 0.70f)
        {
            _playerSounds.PlayRunSound();
        }
        else if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(oldPosition.x, 0, oldPosition.z)) > 0.35f)
        {
            _playerSounds.PlayWalkSound();
        }

        oldPosition = transform.position;
    }
}
