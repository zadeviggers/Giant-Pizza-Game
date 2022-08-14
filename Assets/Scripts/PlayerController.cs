using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{    // Player health points. Each point is "half a heart".
    public int maxHealth;
    public int health;


    // Variables set in Unity editor UI
    public float walkSpeed;
    public float jumpSpeed;

    // Components
    private Rigidbody2D rb;
    private new Collider2D collider;

    // Other controller scripts
    HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float horizontalMovement = 0;
        if (horizontalInput != 0) horizontalMovement = Time.deltaTime * walkSpeed;
        if (horizontalInput < 0) horizontalMovement = horizontalMovement * -1;

        float verticalMovement = 0;
        // Debug.Log(IsGrounded());
        // if (verticalInput > 0 && IsGrounded()) verticalMovement = jumpSpeed;

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

        rb.AddForce(movement);
    }


    // bool IsGrounded()
    // {
    //     float distToGround = collider.bounds.extents.y;
    //     return Physics2D.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Painful"))
        {
            Destroy(other.gameObject);
            TakeDamage();
            return;
        }

        if (other.CompareTag("Healing"))
        {
            Destroy(other.gameObject);
            HealDamage();
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("VeryPainful"))
        {
            TakeDamage(2);
            return;
        }
    }

    void TakeDamage(int amount = 1)
    {
        health -= amount;

        healthBar.UpdateHealthBar();

        if (health < 1)
            Die();
    }

    void HealDamage(int amount = 1)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);

        healthBar.UpdateHealthBar();
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
