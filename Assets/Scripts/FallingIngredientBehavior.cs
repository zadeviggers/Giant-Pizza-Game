using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIngredientBehavior : MonoBehaviour
{
    public Vector3 fallAmount;
    public float destroyBelowY;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(fallAmount);

        if (transform.position.y < destroyBelowY)
        {
            Destroy(gameObject);
        }
    }
}
