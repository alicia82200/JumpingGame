using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_LoadPreferences : MonoBehaviour
{
    #region Variables
    //VOLUME
    [Space(20)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;

    [Space(20)]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;
    #endregion

    private void Awake()
    {
        Debug.Log("Loading player prefs test");

        if (canUse)
        {
            //VOLUME
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeText.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }
        }
    }
}
