using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {

    public Transform followX;

    int WANDERER_INIT_X;
    int WANDERER_INIT_Y = -30; 
    public int ORBIT_RADIUS = 100;
    public float rotationVelocity = 3; //-3 12S
    public bool doesRotate = true; 

    void Start()
    {
        rotationVelocity = Mathf.Abs(rotationVelocity);
        

        WANDERER_INIT_Y = (int)GetComponent<Transform>().position.y;

        if(followX != null)
            WANDERER_INIT_X = (int)followX.position.x;
        else
            WANDERER_INIT_X = (int)GetComponent<Transform>().position.x;



    }
    void SunMovement()
    {
 
      
        if (GetComponentInParent<Weather>().sigma < -2 * Mathf.PI)
            GetComponentInParent<Weather>().sigma = 0;

        if(followX != null)
            WANDERER_INIT_X = (int)followX.position.x; 

        GetComponentInParent<Weather>().sigma = GetComponentInParent<Weather>().sigma - (7.24f * Mathf.Pow(10, -rotationVelocity)); //CHANGE POW to change velocity 10 ^ -3 ==> 12S 1 cicle; -4 ==> 2min ... 
        if(doesRotate)
            GetComponent<Transform>().position = new Vector2( WANDERER_INIT_X + ORBIT_RADIUS * 2 * Mathf.Cos(GetComponentInParent<Weather>().sigma), WANDERER_INIT_Y + ORBIT_RADIUS * Mathf.Sin(GetComponentInParent<Weather>().sigma));


    }

    


     


 
    void LateUpdate () {
      


            SunMovement();
        		
	}
}
