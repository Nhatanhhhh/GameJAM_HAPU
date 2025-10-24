using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        // Gán sự kiện cho nút
        if (playButton != null) playButton.onClick.AddListener(OnPlayClicked);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitClicked);
    }

    public void OnPlayClicked()
    {
        // Tạm thời load scene chọn map
        SceneManager.LoadScene("SelectMap");
        Debug.Log("Bắt đầu chơi game...");
        AudioManager.Instance.PlaySFX("clickSFX");
        Debug.Log("Phát hiệu ứng âm thanh: clickSFX");


    }

    public void OnExitClicked()
    {
        Debug.Log("Thoát game...");
        Application.Quit();

        // Khi test trong Editor, tắt Play Mode luôn (chỉ hoạt động trong Unity Editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
