using UnityEngine;

public class DoorSpawner : MonoBehaviour {
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private DoorType doorType;

    private void Start() {
        if (doorType == DoorType.Random) {
            bool doorTypeRNG = Random.Range(0, 2) == 0;

            if (doorTypeRNG) {
                SpawnExitDoor();
            }
            else {
                SpawnLockedDoor();
            }
        }
        else if (doorType == DoorType.ExitDoor) {
            SpawnExitDoor();
        }
        else if (doorType == DoorType.LockedDoor) {
            SpawnLockedDoor();
        }        
    }

    public void SpawnExitDoor() {
        if (lockedDoor != null) {
            Destroy(lockedDoor);
        }
    }
    public void SpawnLockedDoor() {
        if (exitDoor != null) {
            Destroy(exitDoor);
        }
    }
}

public enum DoorType {
    None,
    Random,
    ExitDoor,
    LockedDoor
}
