using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.None;

    public UnityEvent OnMenu;
    public UnityEvent OnSelectMap;
    public UnityEvent OnPlaying;
    public UnityEvent OnGameOver;

    private void Awake()
    {
        Debug.Log("🟢 GameManager Start");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeState(GameState.Menu);
        Debug.Log("🟢 GameManager Initialized with state: " + CurrentState);
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"⚙️ ChangeState({newState}) — AudioManager.Instance = {(AudioManager.Instance == null ? "NULL" : "OK")}");

        if (newState == CurrentState) return;

        CurrentState = newState;
        Debug.Log("📘 Game State changed to: " + newState);

        switch (newState)
        {
            case GameState.Menu:
                OnMenu?.Invoke();
                AudioManager.Instance.PlayMusic("menu");
                Debug.Log("🎵 Playing menu music");
                break;

            case GameState.SelectMap:
                OnSelectMap?.Invoke();
                AudioManager.Instance.PlayMusic("selectmap");
                Debug.Log("🎵 Playing SelectMap music");
                break;

            case GameState.Playing:
                OnPlaying?.Invoke();
                AudioManager.Instance.PlayMusic("game");
                break;

            case GameState.GameOver:
                OnGameOver?.Invoke();
                AudioManager.Instance.PlayMusic("gameover");
                break;
        }
    }

    // Shortcut methods
    public void StartGame() => ChangeState(GameState.Playing);
    public void ToSelectMap() => ChangeState(GameState.SelectMap);
    public void BackToMenu() => ChangeState(GameState.Menu);
    public void GameOver() => ChangeState(GameState.GameOver);
}
