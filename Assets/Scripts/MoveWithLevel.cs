using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithLevel : MonoBehaviour
{
    private Vector3 MoveAmount = new Vector3(0.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Use Vector3 instead of Vector2 to account for layering
        transform.position = new Vector3(transform.position.x + MoveAmount.x,  transform.position.y + MoveAmount.y, transform.position.z);
    }
}
