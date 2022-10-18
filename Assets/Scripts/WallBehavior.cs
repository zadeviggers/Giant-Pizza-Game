using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    // The particle system to use to create the indicator particles
    public ParticleSystem collisionParticles;

    int collisionParticleSystemAmount = 20;

    bool playerTouching = false;

    // Start is called before the first frame update
    void Update()
    {
        if (playerTouching)
        {
            CreateBoundryParticles();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = false;
        }
    }

    void CreateBoundryParticles()
    {
        for (int i = 0; i < collisionParticleSystemAmount; i++)
        {
            // Instantiate particle system
            ParticleSystem system = Instantiate(collisionParticles);

            ParticleSystem.ShapeModule shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Rectangle;
            shape.scale = transform.localScale;
            shape.position = transform.position;
        }

    }
}
