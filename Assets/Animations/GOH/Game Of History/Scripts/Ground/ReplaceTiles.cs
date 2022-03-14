using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReplaceTiles : MonoBehaviour
{
    //DESCRIPCION: Hace un cambio del sprite de cada tile manteniendo la estructura general.

    //--jaume: cambiar a array de tiles para el futuro 
    public TileBase normalTileA; 
    public TileBase topTileA;
    public TileBase specialTopTileA;
    

    public TileBase normalTileB; 
    public TileBase topTileB;
    public TileBase specialTopTileB;
    
    //--jaume>: en un futuro se anadiran mas opciones
    [Header("Map Type")]
    [Tooltip("Valid options: 'cobblestone' 'grass'")]
    
    public string ground = "grass"; 
    
    //--jaume: Cambiar el nombre de la funcion por el swap specifico
    private void swapToA()
    {
        
        Tilemap tilemap = GetComponent<Tilemap>(); 
        tilemap.SwapTile(normalTileB, normalTileA);
        tilemap.SwapTile(topTileB, topTileA); 
        tilemap.SwapTile(specialTopTileB, specialTopTileA); 
    }


    //--jaume: Cambiar el nombre de la funcion por el swap specifico
    private void swapToB()
    {
        Tilemap tilemap = GetComponent<Tilemap>(); 
        tilemap.SwapTile(normalTileA, normalTileB);
        tilemap.SwapTile(topTileA, topTileB); 
        tilemap.SwapTile(specialTopTileA, specialTopTileB); 

    } 
     void Start()
    {
        switch(ground)
        {
            case "grass":       swapToA();  break;
            case "cobblestone": swapToB();  break;
            default:            swapToB();  break;            
        }
               
    }
 
}
