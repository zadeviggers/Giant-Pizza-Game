using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{


    static public bool IsGrounded(Transform thingTransform)
    {
        int GroundMask = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D hit = Physics2D.Raycast(thingTransform.position, Vector2.down, 2f, GroundMask);
        return hit.collider != null;
    }
}
