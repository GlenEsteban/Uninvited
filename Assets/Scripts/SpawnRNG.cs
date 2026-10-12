using UnityEngine;

public class SpawnRNG : MonoBehaviour {
    [SerializeField, Range(0, 100)] int spawnRate = 100;
    public int rngState;
    void Start() {
        rngState = Random.Range(0, 100);

        if (rngState >= spawnRate) {
            Destroy(gameObject);
        }
    }
}
