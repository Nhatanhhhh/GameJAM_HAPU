using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private string sceneToLoad = "Map1";

    private void Start()
    {
        // Gán sự kiện cho nút
        if (playButton != null) playButton.onClick.AddListener(OnPlayClicked);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitClicked);
    }

    public void OnPlayClicked()
    {
        // Tạm thời load scene chọn map
        SceneLoader.Instance.LoadScene(sceneToLoad);
        AudioManager.Instance.PlaySFX("clickSFX");
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
