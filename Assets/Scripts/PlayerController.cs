using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;
    public float jumpSpeed = 1.0f;
    public float rotateAngle = 90.0f;
    private Rigidbody2D rigidBody2D;
    private GameManager gameManager;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!gameManager.GetIsGameEnd())
        {
            transform.Rotate(Vector3.back, rotateAngle * Time.deltaTime, Space.World);
        
            if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            {
                Debug.Log("JUMP");
                // rigidBody2D.AddForce(Vector3.up * Time.deltaTime * jumpSpeed);
                rigidBody2D.velocity = Vector2.up * jumpSpeed;
            }
        }
    }

}
