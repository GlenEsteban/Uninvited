using System.Collections.Generic;
using UnityEngine;

public class MutualSpawnRPG : MonoBehaviour {
    [SerializeField] private List<GameObject> spawnableObjects;

    private void Start() {
        if (spawnableObjects == null) { return; }

        int numberobjectsToSpawn = Random.Range(0, spawnableObjects.Count);

        foreach (GameObject obj in spawnableObjects) {

        }
    }
}
