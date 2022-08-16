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
    public float collisionExplosionRadius;
    public float collisionExplosionForce;

    // Components
    private Rigidbody2D rb;
    private new Collider2D collider;

    // Other controller scripts
    HealthBar healthBar;


    // Layer mask for ground
    int GroundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();

        GroundMask = 1 << LayerMask.NameToLayer("Ground");
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
        Debug.Log(IsGrounded());
        if (verticalInput > 0 && IsGrounded()) verticalMovement = jumpSpeed;

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

        rb.AddForce(movement);
    }


    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, GroundMask);
        return hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision(other.collider);
    }

    void HandleCollision(Collider2D other)
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
        if (other.CompareTag("VeryPainful"))
        {
            TakeDamage(2);
            Vector2 direction = -rb.velocity;
            float wearoff = 1 - (direction.magnitude / collisionExplosionRadius);
            rb.AddForce(direction.normalized * collisionExplosionForce * wearoff, ForceMode2D.Impulse);
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
