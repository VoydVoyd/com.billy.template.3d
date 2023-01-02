using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionItem
{
    public ResolutionItem(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public int Width { get; }
    public int Height { get; }
}

public class ResolutionSettings : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private readonly List<ResolutionItem> resolutions = new List<ResolutionItem>()
    {
        new ResolutionItem(640, 360),
        new ResolutionItem(960, 540),
        new ResolutionItem(1024, 576),
        new ResolutionItem(1280, 720),
        new ResolutionItem(1600, 900),
        new ResolutionItem(1920, 1080),
        new ResolutionItem(2048, 1152),
        new ResolutionItem(2560, 1440),
        new ResolutionItem(3200, 1800),
        new ResolutionItem(3840, 2160)
    };
    private const string PrefsWidthKey = "resolutionWidth";
    private const string PrefsHeightKey = "resolutionWidth";
    private const string PrefsFullScreenKey = "fullscreenOn";
    private const int InvalidResolution = -4;

    private bool fullScreenOn;
    private int selectedResolution;

    private int prevSelectedResolution;
    private bool prevFullscreenOn;

    private void Awake()
    {
        List<TMP_Dropdown.OptionData> resolutionOptions = new List<TMP_Dropdown.OptionData>();
        foreach (var resolution in resolutions)
        {
            resolutionOptions.Add(new TMP_Dropdown.OptionData($"{resolution.Width} x {resolution.Height}"));
        }

        resolutionDropdown.options = resolutionOptions;
    }

    private void Start()
    {
        int resIndex = GetResolutionIndex(Screen.width, Screen.height);
        
        if (resIndex == InvalidResolution)
        {
            resolutions.Add(new ResolutionItem(Screen.width, Screen.height));
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData($"{Screen.width} x {Screen.height}"));
            int endIndex = 0;
            resolutionDropdown.value = endIndex; 
            selectedResolution = endIndex;
            prevSelectedResolution = endIndex;
        }
        else
        {
            resolutionDropdown.value = resIndex;
            selectedResolution = resIndex;
            prevSelectedResolution = resIndex;
        }

        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
        fullScreenOn = Screen.fullScreen;

        LoadSettings();
    }

    public void SetFullScreenMode(bool on) => fullScreenOn = on;
    public void SetResolution(int index) => selectedResolution = index;

    public void ApplySettings()
    {
        if (selectedResolution >= resolutions.Count || selectedResolution < 0)
            Debug.Log($"ResolutionSettings: Invalid index {selectedResolution} for resolution");
        else
        {
            ResolutionItem resolution = resolutions[selectedResolution];
            Screen.SetResolution(resolution.Width, resolution.Height, fullScreenOn);

            // PlayerPrefs.SetInt(PrefsWidthKey, resolution.Width);
            // PlayerPrefs.SetInt(PrefsHeightKey, resolution.Height);
            // PlayerPrefs.SetInt(PrefsFullScreenKey, fullScreenOn ? 1 : 0);
        }

        prevSelectedResolution = selectedResolution;
        prevFullscreenOn = fullScreenOn;
    }

    public void RevertSettings()
    {
        selectedResolution = prevSelectedResolution;
        fullScreenOn = prevFullscreenOn;

        fullscreenToggle.isOn = fullScreenOn;
        resolutionDropdown.value = prevSelectedResolution;
    }
    
    public void LoadSettings()
    {

        if (PlayerPrefs.HasKey(PrefsWidthKey) && PlayerPrefs.HasKey(PrefsHeightKey) &&
            PlayerPrefs.HasKey(PrefsFullScreenKey))
        {
            int width = PlayerPrefs.GetInt(PrefsWidthKey);
            int height = PlayerPrefs.GetInt(PrefsHeightKey);
            int fullScreen = PlayerPrefs.GetInt(PrefsFullScreenKey);
            
            if (fullScreen != 0 && fullScreen != 1)
                Debug.LogError("Invalid Fullscreen value loaded from PlayerPrefs");
            else
            {
                Screen.SetResolution(width, height, fullScreen == 1);
            }
        }
    }

    private int GetResolutionIndex(int width, int height)
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            ResolutionItem resolution = resolutions[i];
            if (resolution.Width == Screen.width &&
                resolution.Height == Screen.height)
            {
                return i;
            }
        }

        return -1;
    }
}