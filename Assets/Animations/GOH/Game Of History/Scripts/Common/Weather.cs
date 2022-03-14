using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorCicle

{


    private Color _ZenithColour;
    private Color _DawnColour;
 
    private float _red;
    private float _green;
    private float _blue;
    private float _alfa;

    //<maybe change to variable so I can manualy ajust>
    public float velocity = 0.01f; 
     
 
    public void Init(Color ZenithColour, Color DawnColour)
    {
        _ZenithColour = ZenithColour;
        _DawnColour = DawnColour;

        InitRGBA();

    }
    public void InitRGBA()
    {
        _red = 0;
        _green = 0;
        _blue = 0;
        _alfa = 0;
    }
    public float ColourDirection(float colourdimension, float destinydimension)
    {
        if ((destinydimension - colourdimension) > 0) //add colour
        {
            colourdimension = colourdimension + velocity;
        }
        else
        {
            colourdimension = colourdimension - velocity;
        }
        return colourdimension;
    }

    public Color ChangeColour(int reverse, Color ActualColour)
    {


        if (reverse == 1)
        {
            ActualColour.r = ColourDirection(ActualColour.r,_ZenithColour.r);

            ActualColour.g = ColourDirection(ActualColour.g, _ZenithColour.g);

            ActualColour.b = ColourDirection(ActualColour.b, _ZenithColour.b);

            ActualColour.a = ColourDirection(ActualColour.a, _ZenithColour.a);
        }

        else
        {
            ActualColour.r = ColourDirection(ActualColour.r, _DawnColour.r);

            ActualColour.g = ColourDirection(ActualColour.g, _DawnColour.g);

            ActualColour.b = ColourDirection(ActualColour.b, _DawnColour.b);

            ActualColour.a = ColourDirection(ActualColour.a, _DawnColour.a);

        }


 
        return ActualColour;


    }

    public void ColourCounter()
    {

        _red = _red + velocity; 
        _green = _green + velocity;
        _blue = _blue + velocity;
        _alfa = _alfa + velocity;
    }



}


 
public class Weather : MonoBehaviour {


    /*
    * init position of sun = ( -100, -30 )
    * sun radius (200) -> final x position (300)
    * init position of ground = (0, 0)
    * */
    

    public float sigma = -Mathf.PI; // sun_degrees
 
    void FixedUpdate()
    {


 


    }


     
}
