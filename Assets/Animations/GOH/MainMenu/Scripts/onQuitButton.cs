using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onQuitButton : MonoBehaviour
{
    public void doExitGame() 
    {
        Debug.Log("Exit game...");
        Application.Quit();
    }
}
