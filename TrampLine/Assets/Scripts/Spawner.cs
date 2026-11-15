using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;

    public List<GameObject> tramps;
    void OnSpawnFlat() {
        Vector3 position = spawnPoint.position;
        Quaternion rotation = spawnPoint.rotation;
        GameObject prefab = Instantiate(prefabToSpawn, position, rotation);
        tramps.Add(prefab);

        if (tramps.Count > 5) {
            Destroy(tramps[0]);
            tramps.RemoveAt(0);
        }
    }
     void OnSpawnLeft() {
        Vector3 position = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(0, 0, 45);
        GameObject prefab = Instantiate(prefabToSpawn, position, rotation);
        tramps.Add(prefab);

        if (tramps.Count > 5) {
            Destroy(tramps[0]);
            tramps.RemoveAt(0);
        }
    }
     void OnSpawnRight() {
        Vector3 position = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(0, 0, -45);
        GameObject prefab = Instantiate(prefabToSpawn, position, rotation);
        tramps.Add(prefab);

        if (tramps.Count > 5) {
            Destroy(tramps[0]);
            tramps.RemoveAt(0);
        }
    }
}