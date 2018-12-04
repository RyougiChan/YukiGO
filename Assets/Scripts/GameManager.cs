using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {

    public float designWidth = 5.4f;
    public float designHeight = 10.0f;
    public float distance = -3.2f;
    public float cameraDistance = 10.0f;
    public float[] range = new float[] { 0f, 1.0f };
    public float yukiInstantiateDistance = 2.0f;
    public GameObject yuki_1;
    public GameObject yuki_2;
    public GameObject yuki_3;
    public GameObject yuki_4;
    public Vector3[] cameraViewportPos;
    // public Vector3[] cameraViewportWorldPos;
    public GameObject player;
    public Dictionary<string, Vector3> cameraViewportWorldPos;
    private List<GameObject> yukis;
    private bool isGameEnd;
    public float score;
    public Text scoreText;
    public Text gameoverScoreText;
    public GameObject playPanel;
    public GameObject gameoverPanel;
    public GameObject rankingListPanel;
    private float screenWidth; // width of screen in pixel
    private float screenHeight; // height of screen in pixel
    private float aspectRatio; // ratio od width to height
    private float cameraWidth;
    private RectTransform[] rts;
    private List<RectTransform> scoreRts;
    private List<RectTransform> dateRts;
    private List<Score> rankingList;

    // Use this for initialization
    void Start()
    {
        // Resize the size of camera viewport
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        aspectRatio = screenWidth * 1.0f / screenHeight;
        cameraWidth = Camera.main.orthographicSize * 2 * aspectRatio;
        if (cameraWidth > designWidth)
        {
            Camera.main.orthographicSize = designWidth / 2 / aspectRatio;
        }
        // Init default score
        score = 0;
        // Init yukis list
        yukis = new List<GameObject>();
        if (null != yuki_1) yukis.Add(yuki_1);
        if (null != yuki_2) yukis.Add(yuki_2);
        if (null != yuki_3) yukis.Add(yuki_3);
        if (null != yuki_4) yukis.Add(yuki_4);
        // Init camera viewport pos
        cameraViewportPos = new Vector3[4] 
        {
            new Vector3(1.0f,1.0f,cameraDistance),
            new Vector3(0f,1.0f,cameraDistance),
            new Vector3(0f,0f,cameraDistance),
            new Vector3(1.0f,0f,cameraDistance)
        };
        // Init camera viewport to world pos
        cameraViewportWorldPos = new Dictionary<string, Vector3>()
        {
            { "tr", Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,cameraDistance)) },
            { "tl", Camera.main.ViewportToWorldPoint(new Vector3(0f,1.0f,cameraDistance)) },
            { "bl", Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,cameraDistance)) },
            { "br", Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0f,cameraDistance)) }
        };
        // Init the first yuki
        if(SceneManager.GetActiveScene().name != "StartScene")
        {
            InstantiateYuki();
        }
        rts = rankingListPanel.GetComponentsInChildren<RectTransform>();
        scoreRts = new List<RectTransform>();
        dateRts = new List<RectTransform>();
        foreach (RectTransform rt in rts)
        {
            if (rt.tag == "scoreItem_score")
            {
                scoreRts.Add(rt);
            }
            if (rt.tag == "scoreItem_date")
            {
                dateRts.Add(rt);
            }
        }
        rankingList = new List<Score>();
    }

    #region Game Object Control
    // To instantiate a yuki object
    public void InstantiateYuki()
    {
        float ry = UnityEngine.Random.Range(range[0], range[1]);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.2f, ry, 10f));
        Debug.Log(cameraViewportWorldPos["tr"] + " " + pos);
        if(pos.y + 0.8f > cameraViewportWorldPos["tr"].y)
        {
            pos.y = pos.y - 1.0f;
        }
        else if (pos.y - Mathf.Abs(distance) - 0.8f < cameraViewportWorldPos["br"].y)
        {
            pos.y = pos.y + Mathf.Abs(distance) + 1.0f;
        }
        Debug.Log(cameraViewportWorldPos["tr"] + " " + pos);
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
        int index = Mathf.RoundToInt(UnityEngine.Random.Range(0, 3));
        return yukis[index];
    }

    // Update the ranking list
    public void UpdateRankingList(string score)
    {
        Score newScore = new Score(score, DateTime.Now);
        string rankingListJson = PlayerPrefs.GetString("RankingList");
        if (string.IsNullOrEmpty(rankingListJson))
        {
            List<Score> tmpRankingList = new List<Score>() { newScore };
            rankingListJson = JsonConvert.SerializeObject(tmpRankingList);
            rankingList = tmpRankingList;
        }
        else
        {
            rankingList = JsonConvert.DeserializeObject<List<Score>>(rankingListJson);
            rankingList.Add(newScore);
        }
        // Add empty item
        if (rankingList.Count < scoreRts.Count)
        {
            int num = scoreRts.Count - rankingList.Count;
            for (int k = 0; k < num; k++)
            {
                rankingList.Add(new Score("0", DateTime.Now));
            }
        }
        // Sort by score.value asc
        rankingList.Sort();
        // Reverse to sort by desc
        rankingList.Reverse();
        // Prefs
        List<Score> newRankingList = Enumerable.Range(0, 5).Select(c => rankingList[c]).ToList();
        Debug.Log("newRankingList: " + JsonConvert.SerializeObject(newRankingList));
        PlayerPrefs.SetString("RankingList", JsonConvert.SerializeObject(newRankingList));

        Debug.Log(JsonConvert.SerializeObject(rankingList));
        foreach (RectTransform rt in scoreRts)
        {
            int index = Convert.ToInt32(Regex.Match(rt.name, @"\d+").Value);
            rt.GetComponent<Text>().text = rankingList[index - 1].value;
        }
        foreach (RectTransform rt2 in dateRts)
        {
            Debug.Log(rt2.name);
            int index = Convert.ToInt32(Regex.Match(rt2.name, @"\d+").Value);
            rt2.GetComponent<Text>().text = rankingList[index - 1].time.ToString("yyyy-MM-dd");
        }
    }

    #endregion

    #region Getter & Setter
    public void SetScore(float score)
    {
        this.score = score;
        scoreText.text = score.ToString("0");
    }

    public float GetScore()
    {
        return score;
    }
    public void SetIsGameEnd(bool isGameEnd)
    {
        this.isGameEnd = isGameEnd;
    }

    public bool GetIsGameEnd()
    {
        return isGameEnd;
    }
    #endregion

    #region Game Handler
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ShowRankingList()
    {
        rankingListPanel.SetActive(true);
    }

    public void HideRankingList()
    {
        rankingListPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void End()
    {
        // Set game status to end
        isGameEnd = true;
        // Set final score
        gameoverScoreText.text = score.ToString("0");
        // Set Game Over Panel active
        gameoverPanel.SetActive(true);
        // Update Score List
        UpdateRankingList(score.ToString("0"));
    }
    #endregion

}
