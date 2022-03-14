using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float DASH_SPEED = 500;
    float coolDownDash = 0f;
    IEnumerator CoolDownDash()
    {
        for (float i = 1f; i > 0; i -= Time.deltaTime)
        {
            coolDownDash = i;

            yield return null;
        }
    }

    void Start()
    {
        
    }
 
    void Update()
    {

        if (Input.GetButtonDown("Dash") && coolDownDash < 0.1f)
        {
            GetComponent<Animator>().Play("Dash");
            GetComponent<Move>().velocity = DASH_SPEED * transform.localScale.x;
            StartCoroutine("CoolDownDash");
        }


    }
}
