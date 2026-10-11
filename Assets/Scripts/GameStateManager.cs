using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }

    public event Action Exploration;
    public event Action RoomTransition;
    public event Action GameWin;
    public event Action GameOver;

    [SerializeField] private SceneLoader sceneLoader;

    public GameState currentGameState;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        currentGameState = GameState.Exploration;
    }
    public void ChangeGameState(GameState state) {
        if (currentGameState == state) { return; }

        currentGameState = state;

        switch (state) {
            case GameState.Exploration:
                RunExploration();
                break;
            case GameState.RoomTransition:
                RunRoomTransition();
                break;
            case GameState.GameWin:
                RunGameWin();
                break;
            case GameState.GameOver:
                RunGameOver();
                break;
        }
    }
    public void RunExploration() {
        Debug.Log("EVENT: Exploration");

        Exploration?.Invoke();
    }
    public void RunRoomTransition() {
        Debug.Log("EVENT: RoomTransition");

        sceneLoader.ReloadScene(1);

        RoomTransition?.Invoke();
    }
    public void RunGameWin() {
        Debug.Log("EVENT: Game Win");

        GameWin?.Invoke();
    }
    public void RunGameOver() {    
        Debug.Log("EVENT: Game Over");

        GameOver?.Invoke();
    }
}