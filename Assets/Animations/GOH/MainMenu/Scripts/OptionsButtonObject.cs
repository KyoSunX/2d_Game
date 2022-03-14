using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButtonObject : MonoBehaviour
{
    public void GotoOptionsScene()
    {
        SceneManager.LoadScene("OptionsScene");
    }
}