using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Vector3 groundCheckOffset;
    public float groundCheckRadius = 0.02f;
    public LayerMask groundLayerMask = -1; // -1 = everything



    
    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + groundCheckOffset, groundCheckRadius, groundLayerMask);

        for (int i = 0; i < colliders.Length; i++  )
        {
            if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
}
