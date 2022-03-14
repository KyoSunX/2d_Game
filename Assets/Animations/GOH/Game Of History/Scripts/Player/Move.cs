using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Range(-1, 1)]
    public float horizontal;
    public float BASE_SPEED = 80;
    public float JUMPING_SPEED = 120; 

    public float speed;
 
    public float Offset = 0;

    public float velocity = 0; 
    void Start()
    {
        speed = JUMPING_SPEED; 
    }

    
    void Move2()
    {
        velocity = Mathf.Lerp(velocity, speed * horizontal , 3f * Time.fixedDeltaTime);
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, transform.GetComponent<Rigidbody2D>().velocity.y);
       
    }

    void ControlMovement()
    {
        if (GetComponent<ControlAnimations>().AnimationHasFinished("Punch")) 
            horizontal = Mathf.Floor((Input.GetAxis("Horizontal") * 100)) / 100;
        else
            horizontal = 0; 

        if (GetComponent<InvertScale>().isActiveAndEnabled && Input.GetAxis("Horizontal") != 0)
            GetComponent<InvertScale>().ChangeScale(); 
        
    }

    private void Update()
    {
     
            ControlMovement();
    }

    void FixedUpdate()
    {
            Move2();
    }
}
