using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


    public GameObject HighscoreText;
    public int currentHighscore = 0;
    public string currentHighscorePlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        GetHighscoreAndName(); //set up known value
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
     
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        Debug.Log(m_Points);
        UpdateHighscore(m_Points); //Update

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void UpdateHighscore(int points)
    {
        if(currentHighscore < points)
        {
            currentHighscore = points;
            currentHighscorePlayer = ValueManager.Instance.enteredName;
            HighscoreText.GetComponent<Text>().text = "Best Score: " + currentHighscorePlayer + ": " + currentHighscore;
            ValueManager.Instance.highscore = m_Points;
            ValueManager.Instance.nameOfHighscorePlayer = currentHighscorePlayer;
            
        }
    }

    public void GetHighscoreAndName() 
    {
        currentHighscore = ValueManager.Instance.highscore;
        currentHighscorePlayer = ValueManager.Instance.enteredName;

        HighscoreText.GetComponent<Text>().text = "Best Score: " + currentHighscorePlayer + ": " + currentHighscore;
    }

    public void BackToMenu()
    {
        ValueManager.Instance.SaveHighscore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
}
