using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float stopRate = 0.05f;
    public DetectedZone zoneAttack;
    public DetectedZone groundDetect;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //Flip

                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
                _walkDirection = value;
            }
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
           
            animator.SetBool(AnimationString.hasTarget, value);
            _hasTarget = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall || groundDetect.detectedColliders.Count == 0)
        {
            Flip();
        }
        if (CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopRate), rb.velocity.y);
        }
    }

    private void Flip()
    {

        if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }

    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = zoneAttack.detectedColliders.Count > 0; 
    }
}
