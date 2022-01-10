using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    float timer;
    public GameObject bananaPrefab;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            timer = 0;
            float x = Random.Range(-3f, 3f);
            Vector3 position = new Vector3(x, 0, 0);
            Quaternion rotation = new Quaternion(); //Empty porque no queremos ninguna rotacion.
            Instantiate(bananaPrefab, position, rotation);
        }
    }
}
