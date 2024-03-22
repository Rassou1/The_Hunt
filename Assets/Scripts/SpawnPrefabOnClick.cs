using UnityEngine;

public class SpawnPrefabOnClick : MonoBehaviour
{
    public GameObject lobbyMenu;
    public GameObject PlayerPrefab;
    public GameObject PlayerSpawn;

    public void SpawnLobbyMenuAndDeactivateButton()
    {
        Instantiate(lobbyMenu, transform.position, Quaternion.identity);
        gameObject.SetActive(false); // Deactivate the button
    }
    public void SpawnPlayerPrefabAndDeactivateButton()
    {
        Instantiate(PlayerPrefab, PlayerSpawn.transform.position, Quaternion.identity);
    }
}
