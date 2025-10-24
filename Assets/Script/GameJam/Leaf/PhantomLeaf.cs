using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PhantomLeaf : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Collider vật lý mà player có thể đứng lên")]
    public Collider2D solidCollider;

    [Tooltip("Collider dạng trigger để phát hiện chuột")]
    public Collider2D triggerZone;

    private bool isMouseOver = false;

    private void Awake()
    {
        if (solidCollider == null)
            Debug.LogWarning("⚠️ PhantomLeaf: Chưa gán solidCollider!");

        if (triggerZone == null)
            Debug.LogWarning("⚠️ PhantomLeaf: Chưa gán triggerZone!");

        // Ban đầu: tắt collider vật lý
        if (solidCollider != null)
            solidCollider.enabled = false;
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        if (solidCollider != null)
            solidCollider.enabled = true;

        Debug.Log($"🌿 PhantomLeaf: Chuột vào {name} → Bật collider!");
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        if (solidCollider != null)
            solidCollider.enabled = false;

        Debug.Log($"💨 PhantomLeaf: Chuột rời {name} → Tắt collider!");
    }
}
