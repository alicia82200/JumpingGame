using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    #region Default Values
    [Header("Default Menu Values")]
    [SerializeField] private float defaultVolume = 0;

    [Header("Levels To Load")]
    public string level;

    public struct SavedData
    {
        public string playerLevel;
        public int playerScore;
        public int playerDeath;
        public int playerTime;
    }
    
    private SavedData playerData;

    [SerializeField] private int menuNumber;
    #endregion

    #region Menu Dialogs
    [Header("Main Menu Components")]
    [SerializeField] private GameObject menuDefaultCanvas;
    [SerializeField] private GameObject GeneralSettingsCanvas;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject confirmationMenu;
    [Space(10)]
    [Header("Menu Popout Dialogs")]
    [SerializeField] private GameObject noSaveDialog;
    [SerializeField] private GameObject newGameDialog;
    [SerializeField] private GameObject loadGameDialog;
    #endregion

    #region Slider Linking
    [Space(10)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;
    #endregion

    #region Initialisation - Button Selection & Menu Order
    private void Start()
    {
        menuNumber = 1;
    }
    #endregion

    //MAIN SECTION
    #region Confrimation Box
    public IEnumerator ConfirmationBox()
    {
        confirmationMenu.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationMenu.SetActive(false);
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuNumber == 2 || menuNumber == 7 || menuNumber == 8)
            {
                GoBackToMainMenu();
                ClickSound();
            }

            else if (menuNumber == 3 || menuNumber == 4 || menuNumber == 5)
            {
                GoBackToOptionsMenu();
                ClickSound();
            }
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    #region Menu Mouse Clicks
    public void  MouseClick(string buttonType)
    {
        if (buttonType == "Sound")
        {
            GeneralSettingsCanvas.SetActive(false);
            soundMenu.SetActive(true);
            menuNumber = 4;
        }

		if(buttonType == "Exit")
		{
			Debug.Log("YES QUIT!");
			Application.Quit();
		}
	
		if(buttonType == "Options")
		{
            menuDefaultCanvas.SetActive(false);
            GeneralSettingsCanvas.SetActive(true);
            menuNumber = 2;
        }
	
		if(buttonType == "LoadGame")
		{
            menuDefaultCanvas.SetActive(false);
            loadGameDialog.SetActive(true);
            menuNumber = 8;
        }
	
		if(buttonType == "NewGame")
		{
            menuDefaultCanvas.SetActive(false);
            newGameDialog.SetActive(true);
            menuNumber = 7;
        }
    }
    #endregion

    #region Volume Sliders Click
    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
        StartCoroutine(ConfirmationBox());
    }
    #endregion

    #region ResetButton
    public void ResetButton(string GraphicsMenu)
    {
        if (GraphicsMenu == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeText.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
    #endregion

    #region Dialog Options
    public void ClickNewGameDialog(string ButtonType)
    {
        if (ButtonType == "Yes")
        {
            SaveGame("FirstScene", 0, 0, 0);
            SceneManager.LoadScene(level);
        }

        if (ButtonType == "No")
        {
            GoBackToMainMenu();
        }
    }

    public void ClickLoadGameDialog(string ButtonType)
    {
        if (ButtonType == "Yes")
        {
           if (PlayerPrefs.HasKey("playerProgress"))
            {
                Debug.Log("LOADING THE SAVED GAME");
                //LOAD LAST SAVED SCENE
                playerData = LoadGame();
                if(playerData.playerLevel == "")
                {
                    Debug.Log("Load Game Dialog");
                    menuDefaultCanvas.SetActive(false);
                    loadGameDialog.SetActive(false);
                    noSaveDialog.SetActive(true);
                }
                else
                {
                    Debug.Log("Scene Name saved :");
                    Debug.Log(playerData.playerLevel);
                    SceneManager.LoadScene(playerData.playerLevel);
                }
            }

            else
            {
                Debug.Log("Load Game Dialog");
                menuDefaultCanvas.SetActive(false);
                loadGameDialog.SetActive(false);
                noSaveDialog.SetActive(true);
            }
        }

        if (ButtonType == "No")
        {
            GoBackToMainMenu();
        }
    }
    #endregion

    #region Back to Menus
    public void GoBackToOptionsMenu()
    {
        GeneralSettingsCanvas.SetActive(true);
        soundMenu.SetActive(false);

        VolumeApply();

        menuNumber = 2;
    }

    public void GoBackToMainMenu()
    {
        menuDefaultCanvas.SetActive(true);
        newGameDialog.SetActive(false);
        loadGameDialog.SetActive(false);
        noSaveDialog.SetActive(false);
        GeneralSettingsCanvas.SetActive(false);
        soundMenu.SetActive(false);
        menuNumber = 1;
    }

    public void ClickQuitOptions()
    {
        GoBackToMainMenu();
    }

    public void ClickNoSaveDialog()
    {
        GoBackToMainMenu();
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
    #region saveDataPlayer
    public void SaveGame( string level, int score, int death, int time)
    {
        SavedData data = new SavedData();
        data.playerLevel = level;
        data.playerScore = score;
        data.playerDeath = death;
        data.playerTime = time;
        string serializedObject = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("playerProgress", serializedObject);
    }
    #endregion
}
