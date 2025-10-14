using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private MapDatabase mapDatabase;
    [SerializeField] private Transform contentParent; // nơi spawn các map item
    [SerializeField] private GameObject levelItemPrefab;

    private void Start()
    {
        PopulateLevels();
    }

    void PopulateLevels()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject); // clear cũ nếu có

        foreach (var map in mapDatabase.maps)
        {
            GameObject item = Instantiate(levelItemPrefab, contentParent);
            var ui = item.GetComponent<LevelItemUI>();
            ui.Setup(map.mapName, map.sceneName, map.thumbnail);
        }
    }

    public void OnBackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
