using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    [SerializeField]
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    CapsuleCollider2D touchingCol;
    Animator animator;

    //Check ground
    public Transform groundCheck;
    public LayerMask groundLayer;
    //Check wall
    public Transform wallCheck;
    public LayerMask wallLayer; 

    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded {
        get
        {
            return _isGrounded;
        }

        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationString.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
  
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }

        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    [SerializeField]   
    
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }

        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationString.isOnCeiling, value);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //IsGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        //IsOnWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        IsGrounded = touchingCol.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0.01;

        IsOnCeiling = touchingCol.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0.01;
    }
}
