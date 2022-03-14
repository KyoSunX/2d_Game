using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSimpleAnim : MonoBehaviour
{ 
    public float walkingThreshold = 3f;
    public float runningThreshold = 20f;

    private List<string> actionAnimations = new List<string>();
    private Vector2 lastPosition;
    private bool isGrounded = false;
    private bool stayGrounded = false; 
    private bool overlapTrigger = false;
    private float timerNotGrounded = 0;

    void Start()
    {
        actionAnimations = new List<string>() {  "Damage", "Die" };
    }

    public bool AnimationHasFinished(string animation)
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animation) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return false;
        return true;
    }

    bool AnimationHasFinished()
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
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)//Controls when jumping is enabled
    {
        isGrounded = false; 
    }

    void AnimatorTree()
    { 
        if (isGrounded)
        { 
            if (AnimationHasFinished() && GetComponent<DuckController>().coolDown > 0.2f && GetComponent<MoveToRB>() != null)
            { 
                if (Mathf.Abs(lastPosition.x - transform.position.x) > 0.01f && GetComponent<MoveToRB>().velocity > runningThreshold)
                    GetComponent<Animator>().Play("Running");

                else if (Mathf.Abs(lastPosition.x - transform.position.x) > 0.01f && GetComponent<MoveToRB>().velocity  > walkingThreshold)
                    GetComponent<Animator>().Play("Walking");

                else  
                    GetComponent<Animator>().Play("idle");

                lastPosition = transform.position;  
            }
        }

        else if (AnimationHasFinished() && timerNotGrounded > 2 * Time.deltaTime)
        {
            GetComponent<Animator>().Play("Jump");
            GetComponent<Animator>().speed = 1;
        } 

        if (GetComponent<DuckController>().lifePoints <= 0)
        { 
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("Die");
            GetComponent<DuckController>().lifePoints = -1;  
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("burn") && !isGrounded)
            overlapTrigger = true; 
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
