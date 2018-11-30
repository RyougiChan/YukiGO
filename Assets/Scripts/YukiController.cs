using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukiController : MonoBehaviour {

    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;
    public float instantiateViewportDistance = 0.5f;
    public float unitScore = 1.0f;
    public float yukiInstantiateDistance;

    private GameManager gameManager;
    private bool hasInstantiateYuki;
    private bool hasAddScore;
    private GameObject yuki;
    private bool isGameEnd;
    private GameObject player;
    public Dictionary<string, Vector3> cameraViewportWorldPos;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        yuki = transform.GetChild(1).gameObject;
        cameraViewportWorldPos = gameManager.cameraViewportWorldPos;
        yukiInstantiateDistance = gameManager.yukiInstantiateDistance;
        player = gameManager.player;
    }

    void FixedUpdate()
    {
        Vector3 originPos = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(originPos);


        // Score++
        if (!hasAddScore && gameObject.tag == "yukiAbove" && gameObject.transform.position.x < player.transform.position.x)
        {
            gameManager.SetScore(gameManager.GetScore() + unitScore);
            hasAddScore = true;
        }
        // Create a new gameobject using prefab.
        if (!hasInstantiateYuki && originPos.x < cameraViewportWorldPos["tr"].x - yukiInstantiateDistance && gameObject.tag == "yukiAbove")
        //if (!hasInstantiateYuki && viewportPos.x < instantiateViewportDistance && gameObject.tag == "yukiAbove")
            {
            Debug.Log("tr.x=" + cameraViewportWorldPos["tr"].x + " x="+originPos.x);
            Debug.Log("tr: "+cameraViewportWorldPos["tr"] +" pos: "+ originPos);
            gameManager.InstantiateYuki();
            hasInstantiateYuki = true;
        }
        // Destroy the gameobject once it is invisible.
        if(viewportPos.x < -0.1)
        {
            Destroy(gameObject);
        }
        isGameEnd = gameManager.GetIsGameEnd();
        if (!isGameEnd)
        {
            // Move the gameobject
            transform.position = new Vector3(originPos.x - speed * Time.deltaTime, originPos.y, originPos.z);
        }
        // Rotate the yuki
        if (null != yuki)
        {
            yuki.transform.Rotate(Vector3.back * Time.deltaTime * 30 * rotateSpeed, Space.Self);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("------- HIT A YUKI -------");
        gameManager.End();
    }

}
