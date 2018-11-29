using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour {

    private Camera mainCamera;
    private EdgeCollider2D edgeCollider2D;
    private Vector2[] points;
    public float cameraDistance = 10.0f;
    public Dictionary<string, Vector3> cameraViewportWorldPos;

    // Use this for initialization
    void Start () {
        // Init
        mainCamera = Camera.main;
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        points = edgeCollider2D.points;
        cameraViewportWorldPos = new Dictionary<string, Vector3>()
        {
            { "tr", Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,cameraDistance)) },
            { "tl", Camera.main.ViewportToWorldPoint(new Vector3(0f,1.0f,cameraDistance)) },
            { "bl", Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,cameraDistance)) },
            { "br", Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0f,cameraDistance)) }
        };
        // Set points' position to camera viewport corners' position
        // This is a box edge, including 5 points. 
        // Points in `edgeCollider2D.points` begin from rightbottom and 
        // get point anticlickwise rotation.
        points[0] = cameraViewportWorldPos["br"];
        points[1] = cameraViewportWorldPos["tr"];
        points[2] = cameraViewportWorldPos["tl"];
        points[3] = cameraViewportWorldPos["bl"];
        points[4] = cameraViewportWorldPos["br"];

        edgeCollider2D.points = points;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

}
