using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleBehavior : MonoBehaviour
{
    public Vector3 moveAmount;
    public float destroyBelowY = -100;
    public float destroyBelowX = -100;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveAmount);

        if (transform.position.y < destroyBelowY || transform.position.x < destroyBelowX)
        {
            Destroy(gameObject);
        }
    }
}
