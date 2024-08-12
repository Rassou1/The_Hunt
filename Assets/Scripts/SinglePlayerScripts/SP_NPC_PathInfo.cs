using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class contains info to create a state for the the NPC state manager. It also turns off the meshes of the objects used to mark the turns in the editor - Love
public class SP_NPC_PathInfo : MonoBehaviour
{
    [SerializeField] public float _speed;
    [SerializeField] public bool _run;
    [SerializeField] public bool _slide;
    [SerializeField] public bool _wait;
    [SerializeField] public float _waitTime;
    [HideInInspector] public Vector3 _pos;
    MeshRenderer _meshRenderer;
    public SP_NPC_PathInfo()
    {
        
    }

    private void Awake()
    {
        _pos = gameObject.transform.position;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    public SP_NPC_State CreateState(SP_NPC_StateManager manager)
    {
        return new SP_NPC_State(_pos, _speed, _run, _slide, _wait, _waitTime, manager);
    }
}
