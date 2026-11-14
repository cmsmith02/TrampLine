using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;

    public void OnSpawn(InputAction.CallbackContext context) {
        if (context.performed) {
            Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : transform.rotation;

            Instantiate(prefabToSpawn, position, rotation);
        }
    }
}