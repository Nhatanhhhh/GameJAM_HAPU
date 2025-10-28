using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PhantomLeaf : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Collider vật lý mà player có thể đứng lên")]
    public Collider2D solidCollider;

    [Tooltip("Collider dạng trigger để phát hiện chuột")]
    public Collider2D triggerZone;

    [Header("Visual")]
    [Range(0f, 1f)] public float phantomOpacity = 0.5f; // độ trong suốt khi phantom
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool isMouseOver = false;

    private void Awake()
    {
        if (solidCollider == null)
            Debug.LogWarning("PhantomLeaf: Chưa gán solidCollider!");

        if (triggerZone == null)
            Debug.LogWarning("PhantomLeaf: Chưa gán triggerZone!");

        // Lấy SpriteRenderer để chỉnh màu
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        // Ban đầu: tắt collider vật lý và set trong suốt
        if (solidCollider != null)
            solidCollider.enabled = false;

        SetOpacity(phantomOpacity);
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;

        if (solidCollider != null)
            solidCollider.enabled = true;

        SetOpacity(1f);

        //Debug.Log($"PhantomLeaf: Chuột vào {name} → Bật collider + full opacity!");
    }

    private void OnMouseExit()
    {
        isMouseOver = false;

        if (solidCollider != null)
            solidCollider.enabled = false;

        SetOpacity(phantomOpacity);

        //Debug.Log($"PhantomLeaf: Chuột rời {name} → Tắt collider + giảm opacity!");
    }

    private void SetOpacity(float alpha)
    {
        if (spriteRenderer == null) return;

        Color c = originalColor;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
