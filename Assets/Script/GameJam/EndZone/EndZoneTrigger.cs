using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EndZoneTrigger : MonoBehaviour
{
    [Header("Snail Setup")]
    [Tooltip("Prefab con Snail sẽ được spawn khi người chơi tới đích")]
    public GameObject snailPrefab;

    [Tooltip("Vị trí spawn của Snail (nơi nó bắt đầu di chuyển)")]
    public Transform snailSpawnPoint;

    [Tooltip("Vị trí đích mà Snail sẽ đi tới")]
    public Transform snailTargetPoint;

    [Tooltip("Tốc độ di chuyển của Snail")]
    public float snailSpeed = 100f;

    private bool triggered = false;
    private GameObject snailInstance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;


            // Gọi Snail xuất hiện
            if (snailPrefab != null && snailSpawnPoint != null)
            {
                snailInstance = Instantiate(snailPrefab, snailSpawnPoint.position, Quaternion.identity);
                StartCoroutine(MoveSnailToTarget());
            }
            else
            {
                Debug.LogWarning("Chưa gán prefab hoặc vị trí cho Snail trong EndZoneTrigger!");
            }
        }
    }

    private System.Collections.IEnumerator MoveSnailToTarget()
    {
        if (snailInstance == null || snailTargetPoint == null)
            yield break;

        // Phát âm thánh
        AudioManager.Instance?.PlaySFX("jumpscare");

        Vector3 startPos = snailInstance.transform.position;
        Vector3 endPos = snailTargetPoint.position;

        // Di chuyển tuyến tính
        while (Vector3.Distance(snailInstance.transform.position, endPos) > 0.05f)
        {
            snailInstance.transform.position = Vector3.MoveTowards(
                snailInstance.transform.position,
                endPos,
                snailSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Dừng lại ở đích 
        snailInstance.transform.position = endPos;
        Debug.Log("Snail đã tới đích!");

        // Gọi GameManager EndZone
        GameManager.Instance.EndZone();
        Debug.Log("EndZoneTriggered");


    }
}
