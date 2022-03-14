using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    private float startOffset;
    public GameObject camera; 
    private float endPosition; 
    private float previousEnd; 
    
    public List <GameObject> lvl; 
    private bool trigger = false; 
    private int i = 0; 
    private bool canDestroy = false; 

    private LinkedList<float> previous = new LinkedList<float>(); 

    // TO DO: OFFSET DE NIVELL POSAR BE NO HARCODED, GURDAR A CHECKPOINT, FER NET CODI 

    public float GetCheckpoint()
    {
        return previous.Last.Value; 
        
    }
    private GameObject FindObjectInLvl(string tagName)
    {
         foreach(Transform child in lvl[i].transform)
        {
            if(child.CompareTag(tagName))
                return child.gameObject; 
        }

        return null; 
    }

 
    private void Start() { 

        var grid = FindObjectInLvl("grid"); 
      
        Instantiate(lvl[i], new Vector2(0,0), Quaternion.identity, this.transform); 
        endPosition = FindObjectInLvl("endposlvl").transform.position.x;
        startOffset = ((grid.transform.GetChild(0).GetComponent<Tilemap>().size.x) * 0.5f) * grid.transform.GetChild(0).GetComponent<Tilemap>().cellSize.x -6; 
    
        previous.AddFirst(endPosition); 
    }

    private void InitNextLvl()
    {
        var grid = FindObjectInLvl("grid");
        startOffset = ((grid.transform.GetChild(0).GetComponent<Tilemap>().size.x) * 0.5f) * grid.transform.GetChild(0).GetComponent<Tilemap>().cellSize.x - 6; 
        endPosition =  endPosition +startOffset + FindObjectInLvl("endposlvl").transform.position.x;    

 
    }

    // private void OnDrawGizmos() {
    //      Gizmos.color = Color.red; 
    //      Gizmos.DrawSphere(new Vector2(previous.First.Value, 0), 15); 
    //      Gizmos.color = Color.blue;
    //      Gizmos.DrawSphere(new Vector2(previous.Last.Value, 0), 10); 
          
         
    // }
     private void Update() {
 
        float height = 2f * camera.GetComponent<Camera>().orthographicSize;
        float width = height * camera.GetComponent<Camera>().aspect;       
        float movingPosition = (width *0.5f + camera.transform.position.x) ; 
        float backMovingPosition = - width * 0.5f + camera.transform.position.x;
        
    

        if(movingPosition > endPosition && i < lvl.Count ) 
        {   
            i++;  
            
            Instantiate(lvl[i], new Vector2(endPosition + startOffset, 0),Quaternion.identity, this.transform); 
            InitNextLvl();

            previous.AddFirst(endPosition); 
            Debug.Log(endPosition); 

           

        }



        if (backMovingPosition > previous.Last.Value)
        {  
            previous.RemoveLast(); 
            Destroy(transform.GetChild(0).gameObject); 
            //previousEnd = endPosition; 
 
        }
 
      

        
            
    }
 
}
