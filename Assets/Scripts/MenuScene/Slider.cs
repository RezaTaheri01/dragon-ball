using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;            // Reference to the Slider
    public TextMeshProUGUI volumeLabelTMP; // Reference to the TextMeshPro for displaying volume
    public AudioSource audioSource;        // Reference to the AudioSource

    void Start()
    {
        // Set the slider's value to the current audio source volume at the start
        volumeSlider.value = audioSource.volume;

        // Add a listener to call the OnVolumeChange method when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        // Update the audio source volume
        audioSource.volume = value;

        // Update the TextMeshPro label text to show the current volume percentage
        volumeLabelTMP.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
