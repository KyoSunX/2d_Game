using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonObject : MonoBehaviour
{
    public void GotoBackScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}