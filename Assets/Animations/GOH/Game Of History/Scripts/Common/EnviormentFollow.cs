using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentFollow : MonoBehaviour
{
    public GameObject player; 

    void Start()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y); 
        
    }
     
    void LateUpdate()
    {


        if (transform.GetComponent<SunRotation>() != null)
        {
           
        }
 
    }
}
