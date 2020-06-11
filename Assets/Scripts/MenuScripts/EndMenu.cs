using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndMenu : MonoBehaviour
{
    #region Variables
    public struct SavedData
    {
        public string playerLevel;
        public int playerScore;
        public int playerDeath;
        public int playerTime;
    }

    private SavedData playerData;

    [SerializeField] private GameObject finalScore;
    #endregion

    [Header("Main Menu Components")]
    [SerializeField] private GameObject menuDefaultCanvas;

    #region Initialisation 
    private void Start()
    {
        playerData = LoadGame();
        Cursor.visible = true;
        ShowStatic();
    }
    #endregion

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    #region ShowScore
    public void ShowStatic()
    {
        finalScore.GetComponent<Text>().text = playerData.playerScore.ToString();
    }
    #endregion

    #region Menu Mouse Clicks
    public void MouseClick(string buttonType)
    {
        if (buttonType == "Exit")
        {
            Debug.Log("YES QUIT!");
            Application.Quit();
        }
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
}