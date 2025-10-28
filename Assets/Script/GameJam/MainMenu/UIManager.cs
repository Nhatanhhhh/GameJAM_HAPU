using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject endZonePanel;

    private bool isPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Ẩn hết các panel khi khởi động
        HideAll();
    }

    void Update()
    {
        // Nghe phím ESC
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenu();
        }
    }

    public void HideAll()
    {
        if (pauseMenuPanel) pauseMenuPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (endZonePanel) endZonePanel.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        // Không cho mở Pause menu nếu không đang Playing
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Playing)
        {
            Debug.Log($"Không thể bật PauseMenu khi đang ở state: {GameManager.Instance?.CurrentState}");
            return;
        }
        AudioManager.Instance?.PlaySFX("resumeSFX");

        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Dừng hoặc tiếp tục game
    }

    public void OnRestart()
    {
        Time.timeScale = 1;
        AudioManager.Instance?.PlaySFX("restartSFX");
        SceneLoader.Instance.ReloadScene();
        HideAll();
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("Main Menu");
        HideAll();
    }

    public void OnQuit()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }

    // -----------------
    // GAME STATE UI
    // -----------------
    public void ShowGameOver()
    {
        Debug.Log("UIManager: Hiện GameOver panel");
        HideAll();
        gameOverPanel.SetActive(true);
    }

    public void ShowEndZone()
    {
        Debug.Log("UIManager: Hiện EndZone panel (Cảm ơn đã chơi)");
        HideAll();

        if (endZonePanel)
            endZonePanel.SetActive(true);

        // Dừng game như Pause
        Time.timeScale = 0;

        // Nếu có player controller → khóa điều khiển
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
                controller.enabled = false;
        }
    }
}
