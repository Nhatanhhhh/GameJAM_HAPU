using UnityEngine;
using System.Collections;

public class LeafPickupBounce : MonoBehaviour
{
    [Header("Cấu hình Bounce")]
    public float bounceScale = 1.15f;    // phóng to tối đa
    public float bounceSpeed = 6f;       // tốc độ co giãn
    public float settleSpeed = 4f;       // tốc độ về scale ban đầu

    private Vector3 originalScale;
    private Coroutine bounceRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void PlayBounce()
    {
        if (bounceRoutine != null)
            StopCoroutine(bounceRoutine);
        bounceRoutine = StartCoroutine(BounceEffect());
    }

    private IEnumerator BounceEffect()
    {
        //Debug.Log($"Lá {name} nảy nhẹ!");
        Vector3 targetScale = originalScale * bounceScale;

        // 1. Phóng to nhanh
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * bounceSpeed;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        // 2. Quay về scale ban đầu
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * settleSpeed;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale;
        bounceRoutine = null;
    }
}
