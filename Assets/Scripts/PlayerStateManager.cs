using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    public Action<int> LevelUp;
    public static PlayerStateManager Instance { get; private set; }

    [field: SerializeField] public int CurrentHealth { get; private set; } = 100;
    [field: SerializeField] public int Level { get; private set; } = 0;

    public void IncrementRoomsExplored() {
        Level ++;

        LevelUp?.Invoke(Level);
    }
    public void ResetState() {
        CurrentHealth = 100;
        Level = 0;
    }
    public void UpdateCurrentHealth(int amount) {
        CurrentHealth = amount;
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
