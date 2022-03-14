using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimations : MonoBehaviour
{
    List<string> actionAnimations = new List<string>();
    public float walkingThreshold = 3f;
    public float runningThreshold = 20f;



    float timerNotGrounded = 0; 
 
    public bool isGrounded = false;


    void Start()
    {
        actionAnimations = new List<string>() { "Punch", "Kick","Spell", "Dash", "Duck", "DuckReversed", "Damage", "Die"};

    }

    public bool AnimationHasFinished(string animation)
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animation) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return false;
        return true;



    }
    public bool AnimationHasFinished(List<string> animationList)
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animationList[i]) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                return false;

        }

        return true;



    }
    public bool AnimationTime(string animation, float time)
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animation) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > time) 
             
            return true;
 
        else  
            return false;
         
    }

    public bool AnimationHasFinished()
    {
        for (int i = 0; i < actionAnimations.Count; i++)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(actionAnimations[i]) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                return false;

        }

        return true;

    }

    private void OnCollisionStay2D(Collision2D collision)//Controls when jumping is enabled
    {

 
        if (collision.otherCollider == GetComponent<EdgeCollider2D>() && collision.contactCount > 0)
        {
            isGrounded = true;
        } 



         
    }

    private void OnCollisionExit2D(Collision2D collision)//Controls when jumping is enabled
    {

 
        if (collision.otherCollider == GetComponent<EdgeCollider2D>() && collision.contactCount == 0)
        {
            isGrounded = false; 
        }
    }

 


    void AnimatorTree()
    { 
        if (GetComponent<Climb>().onLadder)
            GetComponent<Animator>().Play("Climb");
        else
        {

 
            if (Input.GetButtonDown("Punch")   && AnimationHasFinished())
                GetComponent<Animator>().Play("Punch");



            else if (Input.GetButtonDown("Kick") && AnimationHasFinished() || Input.GetButtonDown("Kick") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Punch"))

                GetComponent<Animator>().Play("Kick");



            if (Input.GetButtonDown("Spell") && AnimationHasFinished())
                GetComponent<Animator>().Play("Spell");

            if (isGrounded)
            {

                if (Input.GetButton("Duck") && AnimationHasFinished())
                    GetComponent<Animator>().Play("Duck");

                if (Input.GetButton("Duck") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Duck") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    GetComponent<Animator>().speed = 0;



                else
                {
                    GetComponent<Animator>().speed = 1;

                    if (!AnimationHasFinished("Duck") && Input.GetButtonUp("Duck"))
                        GetComponent<Animator>().Play("DuckReversed"); 
                }




                if (GetComponent<Hurt>().coolDown > 0.2f && AnimationHasFinished())
                {


                    GetComponent<Animator>().speed = 1;
                    if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > runningThreshold && Input.GetAxis("Horizontal") != 0)
                        GetComponent<Animator>().Play("Running");

                    else if (Mathf.Floor(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)) > 0  && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) > 1f)
                    {
                        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) < runningThreshold * 0.8f && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) > walkingThreshold)
                            GetComponent<Animator>().speed = 1.3f;
                        GetComponent<Animator>().Play("Walking");
                    }


                    else if(Input.GetAxis("Horizontal") != 0 && GetComponent<Rigidbody2D>().velocity.magnitude == 0 || Mathf.Abs(Input.GetAxis("Horizontal"))< 0.1f && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) < 5f)
                    {
                        GetComponent<Animator>().Play("idle");
                        
                    }

                }
            }

            else if (AnimationHasFinished(new List<string>() { "Punch", "Kick", "Spell", "Dash", "DuckReversed", "Damage", "Die" }) && timerNotGrounded > 2* Time.deltaTime)
            {
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().Play("Jump");
       
            }


            if (GetComponent<Hurt>().lifePoints <= 0)
            {
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().Play("Die");
                GetComponent<Hurt>().lifePoints = -1;
            }
 
        

        }


    }
 

   
    void Update()
    {
        AnimatorTree();

        if (!isGrounded)
            timerNotGrounded += Time.deltaTime;
        else
            timerNotGrounded = 0;

 


 
  


    }

}
