using System.Net.Http;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTemplate : MonoBehaviour
{
    public List<string> elements;

    public GameObject camera;

    private float margin;

    [Range(0.0f, 0.3f)]
    public float marginOffset;

    // Start is called before the first frame update
    void Start()
    {
        float height = 2f * camera.GetComponent<Camera>().orthographicSize;
        float width = height * camera.GetComponent<Camera>().aspect;
        int elementsLenght = elements.Count;

        elementView(height, width, elementsLenght);

    }

    public void initMargin(float height, float marginOffset){
        margin = height * marginOffset;
    }

    public void elementView(float height, float width, int elementsLenght) {

        //int newHeight = height / elementsLenght;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
