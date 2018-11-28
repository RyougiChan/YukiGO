using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;

    private void Start()
    {
        Debug.Log("Camera fieldOfView: " + Camera.main.fieldOfView);
        Debug.Log("frustumHeight: " + 2.0f * 10 * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad));
        Debug.Log("Play position: " + transform.position + Camera.main.WorldToViewportPoint(transform.position) + Camera.main.WorldToViewportPoint(new Vector3(1,0,-10)));
    }

    private void FixedUpdate()
    {
        Vector3 originPosition = transform.position;
        transform.position = new Vector3(originPosition.x - speed * Time.deltaTime, originPosition.y, originPosition.z);
    }
}
