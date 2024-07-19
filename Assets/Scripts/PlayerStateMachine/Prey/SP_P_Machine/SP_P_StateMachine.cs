using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_P_StateMachine : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform _bodyRotation;
    [SerializeField] private Transform _cameraRotation;
    [SerializeField] private CapsuleCollider _playerCapsule;
    [SerializeField] private float _mouseSens;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravityStrenght;
    [SerializeField] private float _maxWalkAngle;
    [SerializeField] private Rigidbody _rb;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
