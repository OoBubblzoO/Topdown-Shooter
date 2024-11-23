using UnityEngine;

public class TestObjectForce : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 testKnockback = new Vector2(0, 3f); // Extreme upward force
            rb.AddForce(testKnockback, ForceMode2D.Impulse);
            Debug.Log("Test Knockback Applied : " + testKnockback);
        }
    }
}
