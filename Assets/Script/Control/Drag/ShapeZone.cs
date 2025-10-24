using UnityEngine;

public class ShapeZone : MonoBehaviour
{
    [SerializeField] private ShapeType zoneType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DragItem item = collision.GetComponent<DragItem>();
        if (item == null || item.hasScored) return;

        if (item.shapeType == zoneType)
        {
            // Vật đúng hình dạng, cộng điểm
            DragGameManager.Instance.AddScore(10);
            Debug.Log("Vật đúng hình dạng! +10 điểm");
            item.KYS();
        }
        else
        {
            Debug.Log("Vật sai hình dạng!");
            AudioManager.Instance.PlaySFX("Drag_WrongSFX");
        }

    }
}
