using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float distance = 1.0f;
    public GameObject yuki_1;
    public GameObject yuki_2;
    public GameObject yuki_3;
    public GameObject yuki_4;
    private List<GameObject> yukis;
    private Camera theCamera;

    private Transform tx;

    // Use this for initialization
    void Start()
    {
        yukis = new List<GameObject>();
        if (null != yuki_1) yukis.Add(yuki_1);
        if (null != yuki_2) yukis.Add(yuki_2);
        if (null != yuki_3) yukis.Add(yuki_3);
        if (null != yuki_4) yukis.Add(yuki_4);
        if (!theCamera)
        {
            theCamera = Camera.main;
        }
        tx = theCamera.transform;
        // Instantiate(RandomYuki(), new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log(Camera.main.transform.position.ToString());
        Debug.Log("Screen.width: " + Screen.width);
        Debug.Log("Screen.height: " + Screen.height);
        Debug.Log("UpperLeft: " + GetCorners(10)[0].ToString());
        Debug.Log("UpperRight: " + GetCorners(10)[1].ToString());
        Debug.Log("LoweLeft: " + GetCorners(10)[2].ToString());
        Debug.Log("LowerRight: " + GetCorners(10)[3].ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Get a random yuki in yuki's list
    GameObject RandomYuki()
    {
        int index = Mathf.RoundToInt(Random.Range(0, 3));
        return yukis[index];
    }

    Vector3[] GetCorners(float distance)
    {
        Vector3[] corners = new Vector3[4];

        float halfFOV = (theCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
        float aspect = theCamera.aspect;

        float height = distance * Mathf.Tan(halfFOV);
        float width = height * aspect;

        // UpperLeft
        corners[0] = tx.position - (tx.right * width);
        corners[0] += tx.up * height;
        corners[0] += tx.forward * distance;

        // UpperRight
        corners[1] = tx.position + (tx.right * width);
        corners[1] += tx.up * height;
        corners[1] += tx.forward * distance;

        // LowerLeft
        corners[2] = tx.position - (tx.right * width);
        corners[2] -= tx.up * height;
        corners[2] += tx.forward * distance;

        // LowerRight
        corners[3] = tx.position + (tx.right * width);
        corners[3] -= tx.up * height;
        corners[3] += tx.forward * distance;

        return corners;
    }
}
