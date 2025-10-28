using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Khi bắt đầu kéo
        // Debug.Log("Bắt đầu kéo lá sen.");
        AudioManager.Instance?.PlaySFX("leafDragStartSFX");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Kéo theo chuột
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Kéo xong lá sen.");
    }
}
