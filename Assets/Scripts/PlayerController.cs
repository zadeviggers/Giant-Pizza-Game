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
    Rigidbody2D rb; // Used for movement
    Animator animator; // Used for passing variables to animator

    // Other controller scripts
    HealthBar healthBar;

    // GameManager instance
    GameManager gameManager;

    // Layer mask for ground
    int GroundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();

        GroundMask = 1 << LayerMask.NameToLayer("Ground");

        gameManager = GameManager.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        // Need to make sure to run this every tick to keep animation in sync
        bool grounded = IsGrounded();

        // Get movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Running left & right
        float horizontalMovement = 0;
        if (horizontalInput != 0) horizontalMovement = Time.deltaTime * walkSpeed;
        if (horizontalInput < 0) horizontalMovement = horizontalMovement * -1;

        // Jumping
        float verticalMovement = 0;
        if (grounded && verticalInput > 0) verticalMovement = jumpSpeed;

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

        // Apply movement
        rb.AddForce(movement);

        // Update animator
        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetFloat("XVelocity", rb.velocity.x);
    }


    bool IsGrounded()
    {
        // Raycast down
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, GroundMask);

        // If it didn't hit anything, the player isn't grounded
        bool grounded = hit.collider != null;

        // Update animator
        animator.SetBool("TouchingGround", grounded);

        return grounded;
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
        gameManager.Lose();
    }
}
