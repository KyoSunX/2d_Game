using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void GotoPlayScene()
    {
        SceneManager.LoadScene("TemplateScene");
    }
}

