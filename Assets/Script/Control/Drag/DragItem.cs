using UnityEngine;

public class DragItem : MonoBehaviour
{
    public ShapeType shapeType;
    public bool hasScored = false;


    public void KYS()
    {
        if (hasScored) return;
        hasScored = true;
        Destroy(gameObject);
    }
}
