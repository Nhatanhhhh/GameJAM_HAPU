using UnityEngine;

public class LeafSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [Tooltip("Tốc độ đung đưa (chu kỳ nhanh hay chậm)")]
    public float swaySpeed = 1f;

    [Tooltip("Góc đung đưa tối đa (độ)")]
    public float swayAngle = 5f;

    [Tooltip("Độ lệch pha ban đầu để mỗi lá đung đưa khác nhau")]
    public float randomPhaseOffset = 0f;

    [Tooltip("Ngẫu nhiên hóa offset pha khi bắt đầu")]
    public bool randomizePhase = true;

    private float initialRotation;

    void Start()
    {
        // Lưu rotation ban đầu (theo trục Z)
        initialRotation = transform.localEulerAngles.z;

        // Random lệch pha cho tự nhiên
        if (randomizePhase)
            randomPhaseOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swaySpeed + randomPhaseOffset) * swayAngle;
        transform.localEulerAngles = new Vector3(0f, 0f, initialRotation + angle);
    }
}
