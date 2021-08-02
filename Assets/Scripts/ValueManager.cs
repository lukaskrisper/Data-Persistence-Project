using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueManager : MonoBehaviour
{
    public static ValueManager Instance;
    public string enteredName;
    public GameObject inputField;
    public GameObject textDisplay;

    public string nameOfHighscorePlayer;
    public int highscore;
    public GameObject HighscoreMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterPlayerName()
    {
        enteredName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Your name is: " + enteredName;
    }

    private void Awake()
    {
        //if the object already exists, delete all new ones, we just need one.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateHighscoreMenu(int points, string name)
    {
        HighscoreMenu.GetComponent<Text>().text = "HIGHSCORE: " + name + ": " + points;
    }
}
