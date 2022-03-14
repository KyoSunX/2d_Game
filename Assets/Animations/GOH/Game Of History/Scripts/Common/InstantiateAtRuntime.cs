using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAtRuntime : MonoBehaviour
{
    
    public Vector2 speed; 

    [Range(-10f, 10)]
    public float scaleX;
    [Range(-10f, 10)]
    public float scaleY;


    private Vector2 scale;

    public Vector2 offset;

    [Range(-100, 100)]
    public float gravity;

    [Range(0, 1000)]
    public float mass;

    [Range(0, 100)]
    public float frequency; //if set to 0 instantiate once

    public GameObject instantiatedObject;
    public Transform parent;
    public float timeOnDestroy;

    private float time;


    public bool always;
 
 

    void Start()
    {
    }

    private void InitObject() //is not on start so it can be changed on runtime
    {

        scale = new Vector2(scaleX, scaleY);

        if (instantiatedObject.GetComponent<Rigidbody2D>() != null)
            instantiatedObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
            
        instantiatedObject.transform.parent = parent;
        instantiatedObject.transform.localScale = scale;
     


    }

    public void Instantiate()
    {
        InitObject();
        var obj = Instantiate(instantiatedObject, (Vector2)transform.position + offset, Quaternion.identity, parent);
     
        obj.transform.localScale = scale; 
        
        if (instantiatedObject.GetComponent<Rigidbody2D>() != null)
            obj.GetComponent<Rigidbody2D>().velocity = speed;
        
        Destroy(obj, timeOnDestroy);
       

        
    }

  

    public void InstantiateAlways() //Call on update
    {
        time += Time.deltaTime;
        if (time > frequency)
        {
            Instantiate();
            time = 0;
        }

    }

    void Invert()
    {
        if (transform.localScale.x < 0)
        {
            var vect = GetComponent<InstantiateAtRuntime>().offset;
        
            GetComponent<InstantiateAtRuntime>().offset = new Vector2(-Mathf.Abs(vect.x), vect.y);
            var speed = GetComponent<InstantiateAtRuntime>().speed;
            GetComponent<InstantiateAtRuntime>().speed = new Vector2(-Mathf.Abs(speed.x), speed.y);


        }


        else
        {
            var vect = GetComponent<InstantiateAtRuntime>().offset;
 
            GetComponent<InstantiateAtRuntime>().offset = new Vector2(Mathf.Abs(vect.x), vect.y);
            var speed = GetComponent<InstantiateAtRuntime>().speed;
            GetComponent<InstantiateAtRuntime>().speed = new Vector2(Mathf.Abs(speed.x), speed.y);
        }

    }


    private void Update()
    {
        Invert();
        if (always)
            InstantiateAlways();
 

    }

}
