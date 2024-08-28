using UnityEngine;

public class CamToPlayerConnecter : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraHolder;
    P_StateManager stateManagerScript;

    void Start()
    {
        Transform cameraPos = playerPrefab.transform.Find("CameraPos");
        P_MoveCamera moveCameraScript = cameraHolder.GetComponent<P_MoveCamera>();
        moveCameraScript.cameraPosition = cameraPos;
        stateManagerScript = playerPrefab.GetComponent<P_StateManager>();
        stateManagerScript._cameraOrientation = cameraHolder.transform;
    }

    void Update()
    {
        stateManagerScript._cameraOrientation = cameraHolder.transform;
    }
}
