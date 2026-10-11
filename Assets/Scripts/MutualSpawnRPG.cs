using System.Collections.Generic;
using UnityEngine;

public class MutualSpawnRPG : MonoBehaviour {
    [SerializeField, Range(-1,10)] private int spawnCount = 1;
    [SerializeField] private List<GameObject> spawnableObjects;

    private void Start() {
        DespawnObjectsUntilSpawnCount();

        spawnableObjects.RemoveAll(obj => obj == null);
    }

    private void DespawnObjectsUntilSpawnCount() {
        if (spawnableObjects == null || spawnableObjects.Count == 0) { return; }

        if (spawnCount == -1) {
            spawnCount = Random.Range(0, spawnableObjects.Count); 
        }

        spawnCount = Mathf.Clamp(spawnCount, 0, spawnableObjects.Count - 1);


        while (spawnableObjects.Count > spawnCount) {
            int despawnRNG = Random.Range(0, spawnableObjects.Count);

            Destroy(spawnableObjects[despawnRNG]);
            spawnableObjects.RemoveAt(despawnRNG);
        }
    }
}
