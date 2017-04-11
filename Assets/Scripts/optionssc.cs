using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class optionssc : MonoBehaviour {
    //set up all of the options
    public Toggle fullT;
    public Dropdown ResolutionD;
    public Dropdown TextureD;
    public Dropdown antiaDrop;
    public Dropdown VDrop;
    public Slider MusicVol;
    public Button done;

    //music
    public AudioSource musicScource;
    public AudioSource musicScource1;

    //resolution array
    public Resolution[] resolutions;
    public gamesettings gameset;

    private void OnEnable()
    {
        gameset = new gamesettings();

        //call code on change fo options
        fullT.onValueChanged.AddListener(delegate { onFull(); });
        ResolutionD.onValueChanged.AddListener(delegate { OnresChange(); });
        TextureD.onValueChanged.AddListener(delegate { OntextureChange(); });
        antiaDrop.onValueChanged.AddListener(delegate { OnAAChange(); });
        VDrop.onValueChanged.AddListener(delegate { OnvsinkChange(); });
        MusicVol.onValueChanged.AddListener(delegate { OnmusicvolChange(); });
        done.onClick.AddListener(delegate { onapplybuttonclick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution res in resolutions)
        {
            ResolutionD.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        loadSettings();
    }

    public void onFull()
    {
        //set full screen
        gameset.fullscreen = Screen.fullScreen = fullT.isOn;
    }
    //set resolution
    public void OnresChange()
    {
        Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, Screen.fullScreen);
        gameset.resolution = ResolutionD.value;
    }
    //set texture
    public void OntextureChange()
    {
        QualitySettings.masterTextureLimit = gameset.TexttureQuality = TextureD.value;

    }
    // set antialiasing
    public void OnAAChange()
    {
        QualitySettings.antiAliasing = gameset.antia = (int)Mathf.Pow(2f, antiaDrop.value);
    }
    //set vsink
    public void OnvsinkChange()
    {
        QualitySettings.vSyncCount = gameset.vsink = VDrop.value;
    }
    //set music
    public void OnmusicvolChange()
    {
        musicScource.volume = gameset.music = MusicVol.value;
        musicScource1.volume = gameset.music = MusicVol.value;
    }
    //save setting in json file
    public void onapplybuttonclick()
    {
        saveSettings();
        SceneManager.LoadScene("main");
    }
    public void saveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameset, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }
   // Load the settings from the previous game
    public void loadSettings()
    {
        gameset = JsonUtility.FromJson<gamesettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        fullT.isOn = gameset.fullscreen;
        ResolutionD.value = gameset.resolution;
        TextureD.value = gameset.TexttureQuality;
        antiaDrop.value = gameset.antia - 1;
        VDrop.value = gameset.vsink;
        MusicVol.value = gameset.music;
        Screen.fullScreen = gameset.fullscreen; 

        ResolutionD.RefreshShownValue();

    }


}


