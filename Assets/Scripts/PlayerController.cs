using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;

    private void FixedUpdate()
    {
        Vector3 originPosition = transform.position;
        transform.position = new Vector3(originPosition.x - speed * Time.deltaTime, originPosition.y, originPosition.z);
    }
}
