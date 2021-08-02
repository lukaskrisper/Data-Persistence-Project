using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
        LoadHighscore();
        UpdateHighscoreMenu();
    }

    public void UpdateHighscoreMenu()
    {
        HighscoreMenu.GetComponent<Text>().text = "HIGHSCORE: " + nameOfHighscorePlayer + ": " + highscore;
    }

    // For saving and loading
    [System.Serializable]
    class SaveData
    {
        public string nameOfHighscorePlayerToSaveAndLoad;
        public int pointsOfHighscoreToSaveAndLoad;
    }
    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.nameOfHighscorePlayerToSaveAndLoad = nameOfHighscorePlayer;
        data.pointsOfHighscoreToSaveAndLoad = highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            nameOfHighscorePlayer = data.nameOfHighscorePlayerToSaveAndLoad;
            highscore = data.pointsOfHighscoreToSaveAndLoad;
        }
    }
}
