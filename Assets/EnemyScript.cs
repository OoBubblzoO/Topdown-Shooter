using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player; // reference to player
    public float speed = 2f;

    public int health = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized; // speed stays constant 
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // move towards player at constant speed
        }
    }

    // Reduce health when called, checks health and destroys if at 0
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("ENEMY TOOK DAMAGE: CURRENT HEALTH" + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy enemy at 0 health
        Destroy(gameObject);
    }
}
