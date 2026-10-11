using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    public static PlayerStateManager Instance { get; private set; }

    [SerializeField] private int roomsExplored = 0;

    public void IncrementRoomsExplored() {
        roomsExplored++;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
