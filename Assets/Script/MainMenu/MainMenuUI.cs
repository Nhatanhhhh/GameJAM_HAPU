using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        // Gán sự kiện cho nút
        if (playButton != null) playButton.onClick.AddListener(OnPlayClicked);
        if (settingButton != null) settingButton.onClick.AddListener(OnSettingClicked);
        if (infoButton != null) infoButton.onClick.AddListener(OnInfoClicked);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnPlayClicked()
    {
        // Tạm thời load scene chọn map
        SceneManager.LoadScene("SelectMap");
    }

    private void OnSettingClicked()
    {
        Debug.Log("Setting menu sẽ làm sau");
    }

    private void OnInfoClicked()
    {
        Debug.Log("More Info sẽ làm sau");
    }

    private void OnExitClicked()
    {
        Debug.Log("Thoát game...");
        Application.Quit();

        // Khi test trong Editor, tắt Play Mode luôn (chỉ hoạt động trong Unity Editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
