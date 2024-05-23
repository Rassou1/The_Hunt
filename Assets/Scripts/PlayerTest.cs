using System.Collections;
using Alteruna;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : AttributesSync
{
    AudioSource _audioSoruce;
    Alteruna.Avatar _avatar;
    PlayerWalking _playerSounds;
    Vector3 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        _avatar = GetComponentInParent<Alteruna.Avatar>();
        _audioSoruce = GetComponent<AudioSource>();
        _playerSounds = gameObject.GetComponentInParent<PlayerWalking>();
    }

    // Update is called once per frame

    [SynchronizableMethod]
    public void NonLocalPlayerTest()
    {
        if (_avatar.IsMe)
            return;

        Debug.LogError(_avatar.transform.position);
        _audioSoruce.spatialBlend = 1.0f;

        //if (new Vector3(_avatar.transform.position.x, 0, _avatar.transform.position.z) != new Vector3(oldPosition.x, 0, oldPosition.z))
        //{
        //    if (false)
        //    {
        //        _playerSounds.PlayWalkSound();
        //    }
        //    else
        //    {
        //        _playerSounds.PlayRunSound();
        //    }
        //}

        if (Vector3.Distance(new Vector3(_avatar.transform.position.x, 0, _avatar.transform.position.z), new Vector3(oldPosition.x, 0, oldPosition.z)) > 3)
        {
            _playerSounds.PlayRunSound();
        }
        else if (Vector3.Distance(new Vector3(_avatar.transform.position.x, 0, _avatar.transform.position.z), new Vector3(oldPosition.x, 0, oldPosition.z)) > 0.01f)
        {
            _playerSounds.PlayWalkSound();
        }

        if (new Vector3(0, _avatar.transform.position.y, 0) != new Vector3(0, oldPosition.y, 0))
        {
            //_playerSounds.PlayJumpStartSound();
        }

        oldPosition = _avatar.transform.position;
    }
}
