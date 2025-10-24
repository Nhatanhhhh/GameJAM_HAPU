using UnityEngine;

public class LotusLogic : MonoBehaviour
{
    public string sceneToLoad;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("LotusLogic: Có va chạm với " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("LotusLogic: Player đã chạm vào hoa sen!");

            AudioManager.Instance.PlaySFX("Drag_ScorePointSFX");
            SceneLoader.Instance.LoadScene(sceneToLoad);
        }

    }


}
