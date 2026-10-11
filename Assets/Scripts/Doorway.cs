using UnityEngine;

public class Doorway : MonoBehaviour {
    [SerializeField] private bool isLocked = false;
    [SerializeField] private bool isUnlockable = false;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }

        if (isLocked) {
            print("Locked");
        }
        else {
            print("Enter");
        }
    }
}
