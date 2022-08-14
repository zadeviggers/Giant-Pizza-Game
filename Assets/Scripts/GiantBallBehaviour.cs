using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBallBehaviour : MonoBehaviour
{
    public float rotationAmount;
    private Vector3 rotation;

    public float collisionExplosionRadius;
    public float collisionExplosionForce;

    // Start is called before the first frame update
    void Start()
    {
        rotation = new Vector3(0, 0, rotationAmount);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (!rb) return;
        Vector2 direction = -rb.velocity;
        float wearoff = 1 - (direction.magnitude / collisionExplosionRadius);
        rb.AddForce(direction.normalized * collisionExplosionForce * wearoff, ForceMode2D.Impulse);
    }
}
