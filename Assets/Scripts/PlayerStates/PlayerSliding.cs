using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSliding : MonoBehaviour
{
    public bool isSliding = false;
    public PlayerMovement PL;

    public Rigidbody2D rigidBody;

    public Animator animator;
    public BoxCollider2D regcoll;
    public BoxCollider2D slidecoll;
    public float slideSpeed = 5f;
   
    

    void Update()
    {
        
        

    }
    void OnSlide(InputValue value)
    {
       if(value.isPressed)
       {
        preformSlide();
       } 

    }
    private void preformSlide()
    {
        isSliding = true;

        animator.SetBool ("isSliding", true);

        regcoll.enabled = false;
        slidecoll.enabled = true;
        if(!PL.sprite.flipX)
        {

        rigidBody.AddForce(Vector2.right * slideSpeed);
        }
        else
        {
        rigidBody.AddForce(Vector2.left * slideSpeed);

        }

       
        StartCoroutine("stopSlide");
        
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        animator.Play("Idling");
        animator.SetBool("isSliding",false);
        regcoll.enabled = true;
        slidecoll.enabled = false;
        isSliding = false;
    }
    


}
