using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{    
    [Tooltip("The scale of the item hovering")]
    public float scale = 0.5f; 
    [Tooltip("The distance between the player and the item hovering over")]
    public float offsetY = 16f;  
    [Tooltip("Must have a tag name: 'potion' 'weapon'")]
    public List<string> objectTagList = new List<string>(new string[] {"potion"});
    private bool isEquipped = false;
    void Start()
    { 
        
    }

    private void SetObjectBehaviour(bool active, Transform other)
    {
            foreach (Behaviour childComponent in other.gameObject.GetComponentsInChildren<Behaviour>())
            {
                    childComponent.enabled = active;  
            }
    }
    private void Pick(Collider2D other)
    { 
                other.gameObject.transform.localScale = Vector2.one * scale;  
                other.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + offsetY) ;                  
                other.gameObject.transform.parent = this.gameObject.transform;   
                SetObjectBehaviour(false, other.gameObject.transform); 
                other.gameObject.GetComponent<Animator>().enabled = true;  


 

 
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {   
        foreach (string objName in objectTagList)
        {
            if (other.gameObject.tag == objName && !isEquipped)
            {              
                Pick(other); 
                isEquipped = true;  
            }
        }
    }

    private void Drop()
    {
        if(Input.GetButtonDown("Drop") && isEquipped) //TO DO: Add to Input Manager for joystick 
        {
            isEquipped = false;
            foreach(Transform child in transform)
            {
                foreach(string obj in objectTagList) //drops all objects that have the same name as the objectList 
                {
                    if(child.CompareTag(obj))
                    { 
                        SetObjectBehaviour(true, child.transform); 
                        child.transform.localScale = Vector2.one; 
                        child.gameObject.transform.parent = null; 
                    }


                }
            } 
 
        }
    }

    private void Use()
    {
        //TODO: implement different actions depending on item
    }
    private void Update() {
        Drop(); 
        Use(); 
    }

}
