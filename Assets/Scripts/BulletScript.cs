using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float lifetime = 2f; // seconds it should stay on screen

    // when bullet enters trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.GetComponent<EnemyScript>(); // access enemy script
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }

        //remove game object
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // destroy bullet after set time
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
