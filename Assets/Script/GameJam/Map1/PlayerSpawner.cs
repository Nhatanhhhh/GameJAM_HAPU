using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerController2D playerPrefab;

    public Transform spawnPoint;

    void Awake()
    {
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log($"🟢 PlayerSpawner: Đã sinh ra Player tại {spawnPoint.position}");
        }
        else
        {
            Debug.LogError("❌ PlayerSpawner: Chưa gán playerPrefab trong Inspector!");
        }
    }
}
