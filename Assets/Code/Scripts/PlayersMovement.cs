using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 12f;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Score")]
    private int playerScore=0;

    [Header("Animation")]
    public Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Gravity();
        animator.SetBool("IsJumping", !isGrounded());
    }

    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //Fall increasingly faster
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move( InputAction.CallbackContext context)
    {
        float previousHorizontalMovement = horizontalMovement;
        horizontalMovement = context.ReadValue<Vector2>().x ;

        if(horizontalMovement < previousHorizontalMovement)
        {
            spriteRenderer.flipX = true;
        }
        else if(horizontalMovement > previousHorizontalMovement)
        {
            spriteRenderer.flipX = false;
        }

        if (horizontalMovement != 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        }

    public void Jump( InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            //if (context.performed)
            //{
            //    //Hold down jump button = full height
            //    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            //    //animator.SetBool("IsJumping", false);

            //}
            //else if (context.canceled) 
            //{
            //    //light tap of jump button = half the height
            //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            //}
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            
            
        }
        else
        {
            //animator.SetBool("IsJumping", isGrounded());
           // animator.SetBool("IsJumping", false);
        }
        
    }
    
    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    public void UpdateScore(int score)
    {
        playerScore = score;
        Debug.LogWarning(playerScore);
    }
}
