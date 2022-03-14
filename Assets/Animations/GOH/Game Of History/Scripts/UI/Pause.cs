using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pause : MonoBehaviour
{

    [Tooltip("Pause script works on UI GameObject. Pauses the game with a slowmotion effect, plus a greyscale fading effect. To do this, it needs a blank png that will go from color.a = 0 to color.a = transparency")]
    [Header (" PAUSE SCRIPT ")]
    [Space]
    public string PAUSE; 

    public GameObject greyFilter; 
    
    private bool paused = false; 
    private float transparency; 

    void Start()
    {
        transparency = GetComponentInChildren<SpriteRenderer>().color.a;
       
    }
    IEnumerator Fade()
    {       

            Color tempColor = greyFilter.GetComponent<SpriteRenderer>().color;  
            for (float alpha = 0; alpha <= transparency; alpha += 0.005f)
            {
                greyFilter.GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b, alpha);  
                yield return null;
            }        
    }  
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            paused = !paused;  
            if(paused)
            StartCoroutine("Fade"); 
        } 

        if(paused)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.04f);   
            GetComponentInChildren<SpriteRenderer>().enabled = true;   
        }
   
        else
        { 
            GetComponentInChildren<SpriteRenderer>().enabled = false; 
            Time.timeScale = 1; 
        }
        
    }
}
