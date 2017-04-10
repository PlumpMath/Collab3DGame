using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class optionssc : MonoBehaviour {

    public Toggle fullT;
    public Dropdown ResolutionD;
    public Dropdown TextureD;
    public Dropdown antiaDrop;
    public Dropdown VDrop;
    public Slider MusicVol;
    public Button done;

    public AudioSource musicScource;

    public Resolution[] resolutions;
    public gamesettings gameset;

    private void OnEnable()
    {
        gameset = new gamesettings();

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
        gameset.fullscreen = Screen.fullScreen = fullT.isOn;
    }

    public void OnresChange()
    {
        Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, Screen.fullScreen);
        gameset.resolution = ResolutionD.value;
    }

    public void OntextureChange()
    {
        QualitySettings.masterTextureLimit = gameset.TexttureQuality = TextureD.value;

    }

    public void OnAAChange()
    {
        QualitySettings.antiAliasing = gameset.antia = (int)Mathf.Pow(2f, antiaDrop.value);
    }
    public void OnvsinkChange()
    {
        QualitySettings.vSyncCount = gameset.vsink = VDrop.value;
    }
    public void OnmusicvolChange()
    {
        musicScource.volume = gameset.music = MusicVol.value;
    }

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


