using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SuperPhantomLeaf : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Collider vật lý của SuperPhantom (khi bật sẽ có thể đứng lên)")]
    public Collider2D solidCollider;

    [Tooltip("Collider trigger dùng để phát hiện chuột vào/ra")]
    public Collider2D triggerZone;

    [Tooltip("GameObject mà SuperPhantom đang giữ — bình thường sẽ bật collider của nó")]
    public GameObject heldObject;

    private Collider2D heldCollider;
    private bool isMouseOver = false;

    private void Awake()
    {
        if (solidCollider == null)
            Debug.LogWarning("⚠️ SuperPhantomLeaf: Chưa gán solidCollider!");

        if (triggerZone == null)
            Debug.LogWarning("⚠️ SuperPhantomLeaf: Chưa gán triggerZone!");

        if (heldObject != null)
        {
            heldCollider = heldObject.GetComponent<Collider2D>();
            if (heldCollider == null)
                Debug.LogWarning("⚠️ SuperPhantomLeaf: HeldObject không có Collider2D!");
        }
        else
        {
            Debug.LogWarning("⚠️ SuperPhantomLeaf: Chưa gán heldObject!");
        }

        // Ban đầu: SuperPhantom ẩn (không có collider)
        if (solidCollider != null)
            solidCollider.enabled = false;

        // Ban đầu: vật được giữ thì bật collider
        if (heldCollider != null)
            heldCollider.enabled = true;
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;

        // Hiện SuperPhantom
        if (solidCollider != null)
            solidCollider.enabled = true;

        // Ẩn vật được giữ
        if (heldCollider != null)
            heldCollider.enabled = false;

        Debug.Log($"🌪️ SuperPhantomLeaf: Chuột vào {name} → Bật lá ảo, tắt vật {heldObject?.name}");
    }

    private void OnMouseExit()
    {
        isMouseOver = false;

        // Ẩn SuperPhantom
        if (solidCollider != null)
            solidCollider.enabled = false;

        // Hiện lại vật được giữ
        if (heldCollider != null)
            heldCollider.enabled = true;

        Debug.Log($"💫 SuperPhantomLeaf: Chuột rời {name} → Tắt lá ảo, bật lại vật {heldObject?.name}");
    }
}
