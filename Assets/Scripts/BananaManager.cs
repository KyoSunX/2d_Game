using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BananaManager : MonoBehaviour
{
    public static BananaManager scoreManager;

    public Text scoreText;
    int score = 0;

    private void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("Text").GetComponent<Text>();
            scoreText.text = score + "";
        }
    }

    public void RaiseScore(int a)
    {
        score += a;
        scoreText.text = score + "";
        if (score == 3)
        {
            SceneManager.LoadScene("Scene2");
        }
    }
}
