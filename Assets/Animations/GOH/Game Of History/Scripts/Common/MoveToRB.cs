using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRB : MonoBehaviour
{

    [Range(0, 100)]
    public float velocity;
    public List<GameObject> pivots = new List<GameObject>();
    private int currentPivot = 1;

 
   
    void Start()
    { 

    }

    void FixedUpdate()
    {
  
        if(pivots.Count != 0)
        {
            if(pivots[currentPivot] != null)
            {
                GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(GetComponent<Rigidbody2D>().position, GetComponent<Rigidbody2D>().position + ((Vector2)pivots[currentPivot].transform.position - (Vector2)transform.position).normalized * velocity, 3 * Time.fixedDeltaTime));





                if (Mathf.Abs(Vector2.Distance(transform.position, pivots[currentPivot].transform.position)) < 10f) //Update to next pivot
                {
                    currentPivot++;
                    if (currentPivot > pivots.Count - 1)
                        currentPivot = 0;
                }

            }
            else
            {

                currentPivot++;
            }
        }

        


    }
}
