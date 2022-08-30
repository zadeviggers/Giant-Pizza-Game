using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleBehavior : MonoBehaviour
{
    // Set in inspector
    public Vector2 moveAmount;
    public float destroyBelowY = -100;

    // Constants
    Vector2 moveLeftAmount = new Vector2(-0.01f, 0);

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(moveAmount + moveLeftAmount);

        if (transform.position.y < destroyBelowY)
        {
            Destroy(gameObject);
        }
    }

}
