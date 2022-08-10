using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed;
    public float jumpSpeed;

    private Rigidbody2D rb;
    private new Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadly")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    bool IsGrounded()
    {
        float distToGround = collider.bounds.extents.y;
        return Physics2D.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }
}
