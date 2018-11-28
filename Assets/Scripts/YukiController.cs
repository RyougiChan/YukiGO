using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukiController : MonoBehaviour {

    public float speed = 1.0f;
    public float instantiateViewportDistance = 0.5f;
    private GameManager gameManager;
    private bool hasInstantiateYuki;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        Vector3 originPos = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(originPos);
        if (!hasInstantiateYuki && viewportPos.x < instantiateViewportDistance && gameObject.tag == "yukiAbove")
        {
            Debug.Log("Instantiate Yuki");
            gameManager.InstantiateYuki();
            hasInstantiateYuki = true;
        }
        if(viewportPos.x < -0.1)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(originPos.x - speed * Time.deltaTime, originPos.y, originPos.z);
    }

}
