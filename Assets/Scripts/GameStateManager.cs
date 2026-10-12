using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }

    public event Action Exploration;
    public event Action RoomTransition;
    public event Action GameWin;
    public event Action GameOver;

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject deathScreenPP;

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

        sceneLoader.ReloadScene(0.5f);

        player.GetComponent<Health>().EnableInvincibility(99);
        player.GetComponent<PlayerInput>().enabled = false;

        PlayerStateManager.Instance.IncrementRoomsExplored();

        RoomTransition?.Invoke();
    }
    public void RunGameWin() {
        Debug.Log("EVENT: Game Win");

        GameWin?.Invoke();
    }
    public void RunGameOver() {    
        Debug.Log("EVENT: Game Over");

        player.GetComponent<Health>().EnableInvincibility(99);
        player.GetComponent<PlayerInput>().enabled = false;

        deathScreen.SetActive(true);
        deathScreenPP.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameOver?.Invoke();
    }
    public void RestartGame() {
        PlayerStateManager.Instance.ResetState();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sceneLoader.LoadScene(0, 0);
    }
}