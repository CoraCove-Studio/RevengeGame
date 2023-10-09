using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    public AudioMixer theMixer;
    public TMP_Text mastLabel, musicLabel, sfxLabel;
    public Slider mastSlider, musicSlider, sfxSlider;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        float vol = 0f;
        theMixer.GetFloat("Master Vol", out vol);
        mastSlider.value = vol;

        theMixer.GetFloat("Music Vol", out vol);
        musicSlider.value = vol;

        theMixer.GetFloat("SFX Vol", out vol);
        sfxSlider.value = vol;

        mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }

    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if(selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }


    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;

        if(vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " X " + resolutions[selectedResolution].vertical.ToString();
    }

    public void SetMasterVolume()
    {
        mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();
        theMixer.SetFloat("Master Vol", mastSlider.value);

        PlayerPrefs.SetFloat("Master Vol", mastSlider.value);
    }

    public void SetMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        theMixer.SetFloat("Music Vol", musicSlider.value);

        PlayerPrefs.SetFloat("Music Vol", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFX Vol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFX Vol", sfxSlider.value);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
