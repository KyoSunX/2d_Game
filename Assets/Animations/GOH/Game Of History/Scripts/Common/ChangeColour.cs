using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {



    GameObject WeatherObject;
 
    public Color ZenithColour;
    public Color DawnColour;
 

    public ColorCicle ActualColour = new ColorCicle();
 
    void Start () {

            WeatherObject = this.gameObject;

            ActualColour.Init(ZenithColour, DawnColour);
 
            ColorCicle temp = new ColorCicle();
            temp.Init(ZenithColour, DawnColour);

            WeatherObject.GetComponent<SpriteRenderer>().color = DawnColour;

    }

    void ChangeSceneColours(int reverse)
    {
  
       WeatherObject.GetComponent<SpriteRenderer>().color = ActualColour.ChangeColour(reverse, WeatherObject.GetComponent<SpriteRenderer>().color);
           
    }
    void ColourConfig()
    {
        var getSigma = GetComponentInParent<Weather>().sigma;
        if ((getSigma < -1.25f * Mathf.PI && getSigma > -1.75f * Mathf.PI) || (getSigma < 0 && getSigma > -Mathf.PI))//stable colour hours
        {
            ActualColour.InitRGBA();
       
 
        }

        if (getSigma <= -Mathf.PI && getSigma >= -Mathf.PI * 1.25f) //rising  
        {

            ChangeSceneColours(1);

        }

        else if (getSigma <= -Mathf.PI * 1.75f && getSigma >= -Mathf.PI * 2) //sunset 
        {

            ChangeSceneColours(-1);
        }

    }







    void FixedUpdate()
    {
        var a = 0.001f * Mathf.Pow(10, GetComponent<SunRotation>().rotationVelocity);
  

        if (Time.fixedTime * Mathf.Pow(10, 15) % a == 0) //%velocity of change
        {

            ColourConfig();


        }


    }

}
