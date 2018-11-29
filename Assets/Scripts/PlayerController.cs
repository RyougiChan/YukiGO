using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;
    public float jumpSpeed = 1.0f;
    private Vector3 originPosition;
    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        originPosition = transform.position;
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            Debug.Log("JUMP");
            rigidBody2D.AddForce(Vector3.up * Time.deltaTime * jumpSpeed);
        }
    }

}
