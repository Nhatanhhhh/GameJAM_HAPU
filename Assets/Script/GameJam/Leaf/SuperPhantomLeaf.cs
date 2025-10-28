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

    [Header("Visual Settings")]
    [Range(0f, 1f)]
    public float heldFadeOpacity = 0.5f; // dùng chung cho held object và chính phantom khi phantom

    private Collider2D heldCollider;
    private SpriteRenderer heldRenderer;
    private Color heldOriginalColor;

    private SpriteRenderer selfRenderer;
    private Color selfOriginalColor;

    private bool isMouseOver = false;

    private void Awake()
    {
        if (solidCollider == null)
            Debug.LogWarning("SuperPhantomLeaf: Chưa gán solidCollider!");

        if (triggerZone == null)
            Debug.LogWarning("SuperPhantomLeaf: Chưa gán triggerZone!");

        // Self renderer (để chỉnh opacity cho chính SuperPhantom)
        selfRenderer = GetComponent<SpriteRenderer>();
        if (selfRenderer != null)
            selfOriginalColor = selfRenderer.color;
        else
            Debug.LogWarning("SuperPhantomLeaf: Không tìm thấy SpriteRenderer trên SuperPhantom (để chỉnh opacity).");

        if (heldObject != null)
        {
            heldCollider = heldObject.GetComponent<Collider2D>();
            heldRenderer = heldObject.GetComponent<SpriteRenderer>();

            if (heldCollider == null)
                Debug.LogWarning("SuperPhantomLeaf: HeldObject không có Collider2D!");

            if (heldRenderer != null)
                heldOriginalColor = heldRenderer.color;
            else
                Debug.LogWarning("SuperPhantomLeaf: HeldObject không có SpriteRenderer!");
        }
        else
        {
            Debug.LogWarning("SuperPhantomLeaf: Chưa gán heldObject!");
        }

        // --- Trạng thái ban đầu ---
        if (solidCollider != null)
            solidCollider.enabled = false;

        if (heldCollider != null)
            heldCollider.enabled = true;

        // Mặc định: khi phantom (solid disabled) thì chính nó giảm opacity,
        // lá thật hiện rõ (1f)
        SetHeldOpacity(1f);
        SetSelfOpacity(heldFadeOpacity);
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;

        // Hiện lá ảo (SuperPhantom)
        if (solidCollider != null)
            solidCollider.enabled = true;

        // Ẩn collider của lá thật
        if (heldCollider != null)
            heldCollider.enabled = false;

        // Làm mờ lá thật
        SetHeldOpacity(heldFadeOpacity);

        // Khi SuperPhantom hiển thị (có collider) → chính nó full opacity
        SetSelfOpacity(1f);

        AudioManager.Instance?.PlaySFX("phantomLeafSFX");

        Debug.Log($" SuperPhantomLeaf: Chuột vào {name} → Bật lá ảo, làm mờ {heldObject?.name}");
    }

    private void OnMouseExit()
    {
        isMouseOver = false;

        // Ẩn lá ảo
        if (solidCollider != null)
            solidCollider.enabled = false;

        // Hiện lại collider của lá thật
        if (heldCollider != null)
            heldCollider.enabled = true;

        // Trả lại độ trong suốt ban đầu cho lá thật
        SetHeldOpacity(1f);

        // Khi phantom (không có collider) → giảm opacity của chính nó
        SetSelfOpacity(heldFadeOpacity);

        Debug.Log($"💫 SuperPhantomLeaf: Chuột rời {name} → Tắt lá ảo, khôi phục {heldObject?.name}");
    }

    private void SetHeldOpacity(float alpha)
    {
        if (heldRenderer == null) return;
        Color c = heldOriginalColor;
        c.a = alpha;
        heldRenderer.color = c;
    }

    private void SetSelfOpacity(float alpha)
    {
        if (selfRenderer == null) return;
        Color c = selfOriginalColor;
        c.a = alpha;
        selfRenderer.color = c;
    }
}
