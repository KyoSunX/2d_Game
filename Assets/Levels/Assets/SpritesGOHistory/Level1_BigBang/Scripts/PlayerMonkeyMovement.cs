using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonkeyMovement : MonoBehaviour
{
    public float JumpForce = 10;
    public float Speed = 5;

    private Rigidbody2D playerRigiBody;
    private float Horizontal;
    private bool Grounded;
    // Start is called before the first frame update
    void Start()
    {
        playerRigiBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRigiBody.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        playerRigiBody.velocity = new Vector2(Horizontal * Speed, playerRigiBody.velocity.y);
    }
}
