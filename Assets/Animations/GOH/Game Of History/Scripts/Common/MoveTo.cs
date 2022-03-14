using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    [Range(0, 10)]
    public float velocity;
    public List<GameObject> pivots = new List<GameObject>();
    private int currentPivot = 1; 
    void Start()
    {
        transform.position = pivots[0].transform.position;
    }

    void Update()
    {
         
 
        transform.Translate(((Vector2)pivots[currentPivot].transform.position - (Vector2)transform.position).normalized * velocity);
 



        if (Mathf.Abs( Vector2.Distance(transform.position,pivots[currentPivot].transform.position) )< 3) //Update to next pivot
        {
            currentPivot++;
            if (currentPivot > pivots.Count - 1)
                currentPivot = 0; 
        }
        
    }
}
