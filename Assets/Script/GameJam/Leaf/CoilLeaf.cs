using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoilLeaf : MonoBehaviour
{
    [Header("Config")]
    public float bounceForce = 8f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset vận tốc dọc
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                //Debug.Log($"🌀 CoilLeaf: Đẩy {collision.collider.name} lên với lực {bounceForce}");
                AudioManager.Instance.PlaySFX("coilLeafSFX");
            }
        }
    }
}
