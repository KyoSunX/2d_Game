using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertScale : MonoBehaviour
{

    void Start()
    {
        
    }

    public void ChangeScale()
    {
        if(GetComponent<ControlAnimations>().AnimationHasFinished("Punch") && GetComponent<ControlAnimations>().AnimationHasFinished("Kick") && GetComponent<ControlAnimations>().AnimationHasFinished("Spell") && GetComponent<ControlAnimations>().AnimationHasFinished("Dash"))
        {

            if (Input.GetAxis("Horizontal") < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);

            else
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);


        }
    }
 
 
}
