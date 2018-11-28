using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float distance = -3.2f;
    public float[] range = new float[] { 0.4f, 0.6f };
    public GameObject yuki_1;
    public GameObject yuki_2;
    public GameObject yuki_3;
    public GameObject yuki_4;
    private List<GameObject> yukis;

    // Use this for initialization
    void Start()
    {
        yukis = new List<GameObject>();
        if (null != yuki_1) yukis.Add(yuki_1);
        if (null != yuki_2) yukis.Add(yuki_2);
        if (null != yuki_3) yukis.Add(yuki_3);
        if (null != yuki_4) yukis.Add(yuki_4);
        // Instantiate(RandomYuki(), new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateYuki()
    {
        float ry = Random.Range(range[0], range[1]);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.2f,ry, 10));
        Vector3 posReverse = new Vector3(pos.x, pos.y + distance, pos.z);
        GameObject yukiAbove = Instantiate(RandomYuki(), pos, Quaternion.identity);
        GameObject yukiBelow = Instantiate(RandomYuki(), posReverse, Quaternion.identity);

        yukiAbove.tag = "yukiAbove";
        yukiBelow.tag = "yukiBelow";
        yukiBelow.transform.Rotate(new Vector3(0, 0, 180.0f));

    }

    // Get a random yuki in yuki's list
    GameObject RandomYuki()
    {
        int index = Mathf.RoundToInt(Random.Range(0, 3));
        return yukis[index];
    }

}
