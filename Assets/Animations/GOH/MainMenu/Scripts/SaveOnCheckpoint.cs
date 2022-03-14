using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnCheckpoint : MonoBehaviour
{
    public GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("playerPosition"), 256); 

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("playerPosition", GetComponent<TerrainGenerator>().GetCheckpoint()); 
    }

    private void OnApplicationQuit() {
        
        PlayerPrefs.SetFloat("playerPosition", 0); 
    }
}
