using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    const float DELAYED_INTERVAL = 0.4f; 
    float delay = 0; 
    IEnumerator DelayProjectile()
    {
        for (float currentTime = DELAYED_INTERVAL; currentTime >= 0; currentTime -= 0.1f)
        {

            delay = currentTime;
        

            yield return null; 
        }
    }

    void Invert()
    {
        if (transform.localScale.x == -1)
        {
            GetComponent<InstantiateAtRuntime>().scaleX = -Mathf.Abs(GetComponent<InstantiateAtRuntime>().scaleX);
            var vect = GetComponent<InstantiateAtRuntime>().offset;
            GetComponent<InstantiateAtRuntime>().offset = new Vector2(-Mathf.Abs(vect.x), vect.y);
            var speed = GetComponent<InstantiateAtRuntime>().speed;
            GetComponent<InstantiateAtRuntime>().speed = new Vector2(-Mathf.Abs(speed.x), speed.y);


        }
            

        else
        {
            GetComponent<InstantiateAtRuntime>().scaleX = Mathf.Abs(GetComponent<InstantiateAtRuntime>().scaleX);
            var vect = GetComponent<InstantiateAtRuntime>().offset;
            GetComponent<InstantiateAtRuntime>().offset = new Vector2(Mathf.Abs(vect.x), vect.y);
            var speed = GetComponent<InstantiateAtRuntime>().speed;
            GetComponent<InstantiateAtRuntime>().speed = new Vector2(Mathf.Abs(speed.x), speed.y);
        }

    }

    void Shoot()
    {
    
    
    }

    void Update()
    {

        if(GetComponent<InstantiateAtRuntime>().isActiveAndEnabled) 
            GetComponent<InstantiateAtRuntime>().enabled = false;
 
    
        if ( Input.GetButton("Spell") && GetComponent<ControlAnimations>().AnimationHasFinished("Spell"))
        {
            StartCoroutine("DelayProjectile");
     
        }



        if (delay < 0.001f && !GetComponent<ControlAnimations>().AnimationHasFinished("Spell"))
        {
            Invert();
            delay = DELAYED_INTERVAL;


            GetComponent<InstantiateAtRuntime>().Instantiate();


        }

    
  

    }
}
