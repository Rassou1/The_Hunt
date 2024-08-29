using UnityEngine;

public class CamToPlayerConnecter : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraHolder;
    P_StateManager stateManagerScript;

    void Start()
    {
        Transform cameraPos = playerPrefab.transform.Find("CameraPos");
        Transform cameraBasePos = playerPrefab.transform.Find("CameraBasePos");
        P_MoveCamera moveCameraScript = cameraHolder.GetComponent<P_MoveCamera>();
        moveCameraScript.cameraPosition = cameraPos;
        moveCameraScript.cameraBasePosition = cameraBasePos;
        stateManagerScript = playerPrefab.GetComponent<P_StateManager>();
        stateManagerScript._cameraOrientation = cameraHolder.transform;
    }

    void Update()
    {
        stateManagerScript._cameraOrientation = cameraHolder.transform;
    }
}
