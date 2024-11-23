using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player; // reference to player
    public float speed = 2f;
    public int health = 3;

    public delegate void EnemyDeathHandler(GameObject enemy);
    public event EnemyDeathHandler OnEnemyDeath; // custom event

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find player within the scene (to help with prefabs)
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");

        // Trigger Event | notifies listener in spawner
        OnEnemyDeath.Invoke(gameObject);
        // Destroy enemy at 0 health
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Script | var name | collider2d | check object
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
