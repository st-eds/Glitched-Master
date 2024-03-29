using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    
    Vector2 moveInput;
     public SpriteRenderer sprite;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool isAlive = true;
    float gravityScaleStart;

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleStart = myRigidbody.gravityScale;
    }

    


    void Update()
    {
        if(!isAlive){return;}
        Run();
        FlipSprite();
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Climbing"))) { myAnimator.SetBool("isJumping", false);}
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}
       
        Instantiate(bullet,gun.position,transform.rotation);
       
        return;
        
        

    }

    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}
         if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}
        if(value.isPressed)
        {
            myAnimator.SetBool("isJumping", true);
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
            Debug.Log("jump");

        }
        
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    public void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x + 1), 1f);
        }


    

        }
        void ClimbLadder()
        {
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
            {
                myRigidbody.gravityScale = gravityScaleStart;
                myAnimator.SetBool("isClimbing", false);

                 return;
            }

            Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);

            
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0f;

             bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

            myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

        }
        void Die()
        {

        }
    }


