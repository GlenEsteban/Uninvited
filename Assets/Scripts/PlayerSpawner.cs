using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Transform> spawnPoints;

    private void Start() {
        if (spawnPoints == null || spawnPoints.Count == 0) {
            return;
        }
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);

        playerTransform.position = spawnPoints[randomSpawnPointIndex].position;
    }
}
