using System.Diagnostics;
using System; 
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonObject : MonoBehaviour
{
    public void QuitGame()
    {
        print("QUIT");
        Application.Quit();
    }
}