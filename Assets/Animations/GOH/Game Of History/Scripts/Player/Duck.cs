using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
 
    void Start()
    {
        
    }
     
    void Update()
    {
        if(GetComponent<EdgeCollider2D>().IsTouchingLayers())
        {
            
            if( Input.GetButton("Duck") && !Input.GetButton("Jump") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Punch"))
            {
                GetComponent<Move>().speed = 0;
                GetComponents<BoxCollider2D>()[0].enabled = false;
                GetComponents<BoxCollider2D>()[1].enabled = true;
            }
            else
            {
                GetComponent<Move>().speed = GetComponent<Move>().BASE_SPEED;
                GetComponents<BoxCollider2D>()[0].enabled = true;
                GetComponents<BoxCollider2D>()[1].enabled = false;
            }

        }



    }
}
