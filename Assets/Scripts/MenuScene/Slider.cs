using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;    // Reference to the Slider
    public TextMeshProUGUI volumeLabelTMP;        // Reference to the TextMeshPro for displaying volume

    private const string VolumePrefKey = "MainVolume"; // Key to save and retrieve volume

    void Start()
    {
        // Load the saved volume or set a default value of 0.25 (max volume) if no value is saved
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.5f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        // Add a listener to call the OnVolumeChange method when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        // Update the volume label at the start
        UpdateVolumeLabel(savedVolume);
    }

    void OnVolumeChange(float value)
    {
        // Update the global AudioListener volume
        AudioListener.volume = value;

        // Save the volume value
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();

        // Update the TextMeshPro label text to show the current volume percentage
        UpdateVolumeLabel(value);
    }

    private void UpdateVolumeLabel(float value)
    {
        // Update the volume label text to show the current volume as a percentage
        volumeLabelTMP.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
