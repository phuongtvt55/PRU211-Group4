using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 vecGravity;
    Animator animator;

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    [SerializeField]
    private float jumpInpluse = 8f;
    private float jumpInAir = 7f;
    private float fallMultiple = 1.5f;
    private bool isJumping = false;

    TouchingDirections touchingDirections;
    DamageManage damageManage;
    //Wall slide and wall jump
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    [SerializeField]
    private bool isWallJumping;
    [SerializeField]
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    [SerializeField]
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(8f, 8f);

    //Dash
    public bool canDash = true;
    private bool _isDashing;
    public bool IsDashing
    {
        get
        {
            return _isDashing;
        }
        set
        {
            _isDashing = value;
        }
    }
    private float dashingPower = 15f;
    private float dashingTime = 0.45f;
    private float dashingCooldown = 1f;


    public float CurrentSpeed { 
        get 
        {
            if(CanMove)
            {
                if (IsRunning && !touchingDirections.IsOnWall)
                {
                    return runSpeed;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //Locked movement
                return 0;
            }
        } 
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning { get {
            return _isRunning;
        }
        
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.isRunning, value);
        }
    }

    public bool _isFacingRight = true;
    
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);   
        }
    }

    private bool _castSpell;
    public bool CastSpell
    {
        set
        {
            _castSpell = value;
            animator.SetBool(AnimationString.isCastSpell, value);
        }
        get
        {
            return _castSpell;  
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();    
        damageManage = GetComponent<DamageManage>();    
        //Gravity
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallSlide();
        WallJump();

    }

    private void FixedUpdate()
    {
        //Can't move when dash
        if (_isDashing)
        {
            return;
        }
        //Fix bug wall jump
        if ((moveInput.x < 0 && IsFacingRight) || (moveInput.x > 0 && !IsFacingRight))
        {
            moveInput.x = 0;
            IsRunning = false;
        }

        //Move when is grounded or air
        if (touchingDirections.IsGrounded && CanMove)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        }
        else if (!isWallJumping && !touchingDirections.IsGrounded && moveInput.x != 0 && !isWallSliding && (moveInput.x > 0 && IsFacingRight) || (moveInput.x < 0 && !IsFacingRight))
        {
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        }
       

        //Gravity fall
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiple * Time.deltaTime;
        }
        
        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }

    public void WallSlide()
    {
        //Check player on wall to able to slide
        if(touchingDirections.IsOnWall && !touchingDirections.IsGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Math.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
        animator.SetBool(AnimationString.isWallSliding, isWallSliding);
    }

    public void WallJump()
    {
        if (touchingDirections.IsGrounded)
        {
            isWallJumping = false;
        }
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter = -Time.deltaTime;
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        IsDashing = true;
        damageManage.IsUntouchable = true;
        animator.SetTrigger(AnimationString.isDashingTrigger);
        float originalGra = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);  
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGra;
        IsDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        damageManage.IsUntouchable = false;
        StopCoroutine(Dash());
    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
  
    public void OnRun(InputAction.CallbackContext context)
    {
        if (IsAlive)
        {
            moveInput = context.ReadValue<Vector2>();
            IsRunning = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsRunning = false;
        }
    }

   
    public void OnJump(InputAction.CallbackContext context)
    {      
        if (context.started && touchingDirections.IsGrounded && CanMove && !IsDashing)
        {
          
            animator.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpInpluse);
            isJumping = true;
        }
        else if(context.started && !touchingDirections.IsGrounded && CanMove)
        {
            if (isJumping && !isWallSliding) {
                rb.velocity = new Vector2(rb.velocity.x, jumpInAir);
                isJumping= false;
            }
            //When player wall slide can able to wall jump
            else if(isWallSliding && wallJumpingCounter > 0f)
            {
                animator.SetTrigger(AnimationString.jumpTrigger);
                isWallJumping = true;
                rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = 0f;
                if(transform.localScale.x != wallJumpingDirection)
                {
                    IsFacingRight = !IsFacingRight;
                }
                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !IsDashing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetTrigger(AnimationString.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if(context.started && !IsDashing)
        {
            animator.SetTrigger(AnimationString.rangedAttackTrigger);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && canDash && touchingDirections.IsGrounded && IsRunning)
        {
            StartCoroutine(Dash());
        }
    }

    public void OnCastSpell(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            CastSpell = true;
        }
        else if(context.canceled && touchingDirections.IsGrounded) {
            CastSpell = false;  
        }
        
           
       
    }
}
