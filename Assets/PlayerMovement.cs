using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // player speed
    private Vector2 movement; // stores player movement direction (x, y)
    private Rigidbody2D rb; // reference


    public GameObject bulletPrefab; // Reference to bullet 
    public Transform firePoint; // point where bullets fired
    public float bulletSpeed = 10f; // bullet speed


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");


        // movement returns a -1, 0, or 1 vfalue 
        // positive +1 when D pressed, -1 when A pressed 0 when Nothing pressed (x)
        // positive +1 when W pressed, -1 when S is pressed (Y)
        if (movement.x > 0) // right
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            Debug.Log("Player is facing RIGHT, Rotation: " + transform.rotation.eulerAngles);
        }
        else if (movement.x < 0) // left
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            Debug.Log("Player is facing LEFT, Rotation: " + transform.rotation.eulerAngles);
        }
        else if (movement.y > 0) // up
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Player is facing UP, Rotation: " + transform.rotation.eulerAngles);
        }
        else if (movement.y < 0) // down
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            Debug.Log("Player is facing DOWN, Rotation: " + transform.rotation.eulerAngles);
        }

        // SPACEBAR CHECK

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // scale direction vector by player speed + smooth movement    
    }


    void FireBullet()
    {
        // Instantiate bullet at firepoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed; // fire Forward

        Debug.Log("Bullet Fired");
    }
}