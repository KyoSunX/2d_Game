using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [Range(0, 500)]
        
    public float JumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float jumpingStep = 1f;

 

    private void Start()
    {
 
    }

    private void OnCollisionStay2D(Collision2D collision)//Controls when jumping is enabled
    {
         
        //foreach (ContactPoint2D contact in collision.contacts) //<--gizmos on collision
        //{ 
        //    //print(contact.collider.name + " hit " + contact.otherCollider.name);
        //    Debug.DrawRay(contact.point, contact.normal *2, Color.green);
        //}
         
        if (collision.otherCollider == GetComponent<EdgeCollider2D>() && collision.contactCount > 0)
        {
            Jumping();
        } 
    }
     
     void Jumping()
    {
        if (Input.GetButton("Jump")  && GetComponent<ControlAnimations>().AnimationHasFinished("Damage"))
        { 
            GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpVelocity;
 

        }
    }
    void AccelerateOnJump()
    {
        if (GetComponent<Move>().speed < GetComponent<Move>().JUMPING_SPEED)
        
            GetComponent<Move>().speed += jumpingStep;
        
        if(GetComponent<EdgeCollider2D>().IsTouchingLayers())
            GetComponent<Move>().speed = GetComponent<Move>().BASE_SPEED;
    }
    void BetterJump()
    {

        if ( GetComponent<Rigidbody2D>().velocity.y < 0 )
        {
            AccelerateOnJump();
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if ( GetComponent<Rigidbody2D>().velocity.y > 0 )
        {
            AccelerateOnJump();
            if (!Input.GetButton("Jump"))

                GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
 

    }
 
    void  FixedUpdate()
    {
             
        BetterJump();  
      

    }
 } 