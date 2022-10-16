using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    // The particle system to use to create the indicator particles
    public ParticleSystem collisionParticles;

    // The places to show the particles
    private BoxCollider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        // Get colliders
        colliders = GetComponents<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateBoundryParticles();
        }
    }

    void CreateBoundryParticles() { 
        // Create a particle system for each wall
        foreach (BoxCollider2D collider in colliders)
        {
            // Instantiate particle system
            ParticleSystem system = Instantiate(collisionParticles);

            // Change it's shape to match the wall
            

            ParticleSystem.ShapeModule shape = system.shape;
            shape.shapeType = ParticleSystemShapeType.Rectangle;
            //shape.scale;

        }
    }
}
