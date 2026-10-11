using System.Collections.Generic;
using UnityEngine;

public class MultiDoorSpawnerRNG : MonoBehaviour {
    [SerializeField] private List<DoorSpawner> doorSpawners;

    private void Start() {
        int randomSpawnerIndex = Random.Range(0, doorSpawners.Count);

        for (int i = 0; i < doorSpawners.Count; i++) {
            if (i == randomSpawnerIndex) {
                doorSpawners[i].SpawnExitDoor();
            }
            else {
                doorSpawners[i].SpawnLockedDoor();
            }
        }
    }
}
