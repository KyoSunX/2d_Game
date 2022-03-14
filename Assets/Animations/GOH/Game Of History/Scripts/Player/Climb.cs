using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public float elapsedEscape;
    public float speed; 
    int vertical;
    float velocityY = 0;

    float gravity; 

    public bool onLadder = false;

    private bool triggerTop = false; 
    void Start()
    {
        gravity = GetComponent<Rigidbody2D>().gravityScale; 
    }

 

    void Exit()
    {

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
 
        velocityY = 0;
        onLadder = false;

        GetComponent<Duck>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        GetComponent<InvertScale>().enabled = true;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("top"))
            triggerTop = true; 

    }
 
    private void OnTriggerStay2D(Collider2D collision) 
    {

 
 
      
        if (Input.GetButtonDown("Jump") && onLadder)
        {
            velocityY = Mathf.Lerp(velocityY, speed * 80, 3f * Time.fixedDeltaTime);

            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityY, velocityY);
            Exit();


        }
 
        if(!triggerTop)
        {

            if ((Input.GetAxis("ClimbAxis") > 0 || Input.GetButton("ClimbUp")) && collision.CompareTag("ladder")  && !Input.GetButtonDown("Jump"))
            {
                onLadder = true;
                transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                GetComponent<Duck>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<InvertScale>().enabled = false;



            }

        }





    }


    private void OnCollisionStay2D(Collision2D collision) // get out if hits ground
    {
        if((Input.GetButton("ClimbDown") || Input.GetAxis("ClimbAxis") < 0) && collision.otherCollider == transform.GetComponent<EdgeCollider2D>())
        {
            Exit();

        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("ladder"))
            Exit();

        if (collision.CompareTag("top"))
            triggerTop = false;
    }




    void SetVelocity(int sense)
    {
        vertical = sense;


        GetComponent<Animator>().speed = Mathf.Abs(sense);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, sense * 60);

    }

    void ControlClimb()
    {
        if ((Input.GetButton("ClimbUp")  || Input.GetAxis("ClimbAxis") > 0) && !triggerTop)
            SetVelocity(1);

        else if (Input.GetButton("ClimbDown") || Input.GetAxis("ClimbAxis") < 0)
            SetVelocity(-1);
  
        else
            SetVelocity(0);
      

    }

    void ClimbMovement()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX; 
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.GetComponent<Rigidbody2D>().velocity.x, velocityY);

        ControlClimb();

    }
    void FixedUpdate()
    {
        if(onLadder)
            ClimbMovement();
        if (GetComponent<Hurt>().lifePoints <= 0)
            Exit();


    }

 
}
