using UnityEngine;

public class SpawnPrefabOnClick : MonoBehaviour
{
    public GameObject prefabToSpawn;

    public void SpawnPrefabAndDeactivateButton()
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        gameObject.SetActive(false); // Deactivate the button
    }
}
