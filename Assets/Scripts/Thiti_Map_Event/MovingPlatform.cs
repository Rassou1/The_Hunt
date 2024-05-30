using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thitiwich´s code on how to make a moving platforms:
// Yet again got scrapped because it was too hard to work with the collision the charafcters have.

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private LayerMask affectedLayers;

    [SerializeField]
    private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;


    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >= 2)
        {
            TargetNextWaypoint();
        }
    }

    // How the platforms moves with the help of empty object call wayPoint
    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    other.transform.SetParent(transform);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    other.transform.SetParent(null);                                        
    //}

    // An attempt to make the character move along side the platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger entered: " + other.gameObject.name);
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.parent == transform)
        {
            Debug.Log("Trigger exited: " + other.gameObject.name);
            other.transform.SetParent(null);
        }
    }

  
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (((1 << other.gameObject.layer) & affectedLayers) != 0)
    //    {
    //        other.transform.SetParent(transform);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (((1 << other.gameObject.layer) & affectedLayers) != 0 && other.transform.parent == transform)
    //    {
    //        other.transform.SetParent(null);
    //    }
    //}

}
