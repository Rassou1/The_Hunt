using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class CubeSpawner : MonoBehaviour
{
    private Alteruna.Avatar _avatar;
    private Spawner _spawner;

    [SerializeField] private int indexToSpawn = 0;
    [SerializeField] private LayerMask despawnLayer;
    private void Awake()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        _spawner= GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Spawner>();

    }

    private void Update()
    {
        if (!_avatar.IsMe)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnCube();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DespawnCube();
        }

    }

    public void SpawnCube()
    {
        _spawner.Spawn(indexToSpawn, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, Camera.main.transform.rotation, new Vector3(0.5f, 0.5f, 0.5f));
    }
    public void DespawnCube()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out RaycastHit hit, Mathf.Infinity, despawnLayer))
        {
            _spawner.Despawn(hit.transform.gameObject);
        }
    }
}
