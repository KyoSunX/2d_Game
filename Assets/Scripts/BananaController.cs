using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BananaManager.scoreManager.RaiseScore(1);

        Destroy(transform.parent.gameObject);
    }
}
