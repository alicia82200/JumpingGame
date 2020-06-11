using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.ComponentModel.Design.Serialization;

public class UIControl : MonoBehaviour
{
    #region Default Values
    public struct SavedData
    {
        public string playerLevel;
        public int playerScore;
        public int playerDeath;
        public int playerTime;
    }

    private SavedData playerData;

    int time = 1;
    int comptSec = 0;
    int comptSec2 = 0;
    int intervalTime = 60;
    int sec = 0;
    int min = 0;
    bool addOne = false;
    #endregion

    #region Menu
    [Header("Static Components")]
    [SerializeField] private GameObject activeLevel;
    [SerializeField] private GameObject activeScore;
    [SerializeField] private GameObject activeMin;
    [SerializeField] private GameObject activeSec;

    [Header("UI Components")]
    [SerializeField] private GameObject staticCanvas;
    [SerializeField] private GameObject menuCanvas;
    #endregion

    #region Initialisation
    private void Start()
    {
        playerData = LoadGame();
        min = playerData.playerTime;
        ShowStatic();
        GoBackToGame();
    }
    #endregion

    //MAIN SECTION
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClickSound();
            GoToMenu();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClickSound();
            GoBackToGame();
        }

        comptSec = time;

        if(comptSec > comptSec2)
        {
            comptSec2 = comptSec;
            sec++;

            if(sec == intervalTime)
            {
                sec = 0;
                addOne = true;
            }
        }

        if(sec == 0  &&  addOne)
        {
            min++;
            addOne = false; 
        }

        activeSec.GetComponent<Text>().text = sec.ToString();
        activeMin.GetComponent<Text>().text = min.ToString();

        time = (int)Time.time;
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    #region Menu Mouse Clicks
    public void MouseClicking(string buttonType)
    {
        if (buttonType == "SaveGameAndExit")
        {
            playerData = LoadGame();
            playerData.playerDeath = getDeath();
            playerData.playerTime = min;
            Debug.Log("Level : ");
            Debug.Log(playerData.playerLevel);
            Debug.Log("Score : ");
            Debug.Log(playerData.playerScore);
            Debug.Log("Death : ");
            Debug.Log(playerData.playerDeath);
            SaveGame(playerData.playerLevel, playerData.playerScore, playerData.playerDeath, playerData.playerTime);
            Debug.Log("Game Saved");
            Application.Quit();
        }
    }
    #endregion

    #region Back to Game
    public void GoBackToGame()
    {
        Cursor.visible = false;
        staticCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }
    public void GoToMenu()
    {
        Cursor.visible = true;
        staticCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
    #endregion

    #region ShowLevelAndScore
    public void ShowStatic()
    {
        activeLevel.GetComponent<Text>().text = numScene(playerData.playerLevel).ToString(); 
        activeScore.GetComponent<Text>().text = playerData.playerScore.ToString();
    }
    #endregion

    #region SavaInGame
    public void SaveGame(string level, int score, int death, int time)
    {
        SavedData data = new SavedData();
        data.playerLevel = level;
        data.playerScore = score;
        data.playerDeath = death;
        data.playerTime = time;
        string serializedObject = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("playerProgress", serializedObject);
    }

    public int getDeath()
    {
        GameObject RSzone = GameObject.Find("RespawnZone");         //calculate score
        int death = RSzone.GetComponent<CATeleportTarget>().numDeath();
        return death;
    }
    #endregion

    #region LoadingPrefPlayer
    public SavedData LoadGame()
    {
        string serializedObject = PlayerPrefs.GetString("playerProgress");
        SavedData data = JsonUtility.FromJson<SavedData>(serializedObject);
        return data;
    }
    #endregion

    #region Scenes
    public int numScene(string scene)
    {
        switch (scene)
        {
            case "FirstScene": return 1;
            case "SecondScene": return 2;
            case "ThirdScene": return 3;
            case "FourScene": return 4;
            case "FiveScene": return 5;
            case "SixScene": return 6;
            case "SevenScene": return 7;
            case "EightScene": return 8;
            case "NineScene": return 9;
            case "TenScene": return 10;
            default: return 0;
        }
    }
    #endregion

    public int getTime()
    {
        return min;
    }
}

