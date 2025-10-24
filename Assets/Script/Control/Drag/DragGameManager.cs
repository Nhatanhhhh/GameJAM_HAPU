using UnityEngine;

public class DragGameManager : MonoBehaviour
{
    public static DragGameManager Instance { get; private set; }
    private int score;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"🎯 Score: {score}");
    }
}
