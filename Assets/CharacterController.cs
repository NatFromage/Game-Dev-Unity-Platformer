using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider2D;

    [SerializeField]
    private LayerMask platformLayer;

    private float raycastDistance = 0.3f;

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + raycastDistance, platformLayer);
        return hit.collider != null;
    }

    private Vector2 wallcheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnWall()
    {
        Vector2 rayOrigin = new Vector2(capsuleCollider2D.bounds.center.x + (capsuleCollider2D.bounds.extents.x * wallcheckDirection.x), capsuleCollider2D.bounds.center.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, wallcheckDirection, capsuleCollider2D.bounds.extents.x + raycastDistance, platformLayer);
       return hit.collider != null;
        
    }
}
