using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelItemUI : MonoBehaviour
{
    [SerializeField] private Image thumbnail;
    [SerializeField] private TextMeshProUGUI mapNameText; // nếu dùng Text thay TMP thì đổi type
    [SerializeField] private Button button;

    private string sceneToLoad;

    public void Setup(string name, string sceneName, Sprite image)
    {
        mapNameText.text = name;
        thumbnail.sprite = image;
        sceneToLoad = sceneName;

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
        else
            Debug.LogWarning("Scene name trống trong LevelItemUI");
    }
}
