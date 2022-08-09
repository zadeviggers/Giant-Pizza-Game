using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed;
    public float jumpSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (verticalInput > 0) verticalMovement = jumpSpeed;

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);


    }
}
