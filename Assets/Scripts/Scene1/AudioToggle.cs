using UnityEngine;
using UnityEngine.UI; // Required for Toggle

public class AudioToggle : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource audioSource;
    public AudioSource audioSource2;

    
    // Reference to the Toggle (Checkbox)
    public Toggle audioToggle;

    private const string TogglePrefKey = "AudioToggleState"; // Key for saving the toggle state

    void Start()
    {
        // Ensure the Toggle is linked and add a listener to its value change event
        if (audioToggle != null)
        {
            audioToggle.onValueChanged.AddListener(OnToggleValueChanged);
            
            // Load the toggle state from PlayerPrefs and update the UI
            bool isToggleOn = PlayerPrefs.GetInt(TogglePrefKey, 1) == 1; // Default to true if not found
            audioToggle.isOn = isToggleOn;

            // Ensure the audio state is correct based on the initial state of the checkbox
            UpdateAudioState(isToggleOn);
        }
    }

    // This method will be called whenever the checkbox (toggle) value changes
    private void OnToggleValueChanged(bool isChecked)
    {
        UpdateAudioState(isChecked);
        
        // Save the toggle state in PlayerPrefs
        PlayerPrefs.SetInt(TogglePrefKey, isChecked ? 1 : 0);
        PlayerPrefs.Save(); // Save changes immediately
    }

    // Method to update the audio playback based on the toggle state
    private void UpdateAudioState(bool isPlaying)
    {
        if (isPlaying)
        {
            if (!audioSource.isPlaying)
                audioSource.Play(); // Play the audio
            audioSource.mute = false; // Ensure it's unmuted when playing
            audioSource2.mute = false; // Mute the audio instead of pausing it
        }
        else
        {
            audioSource.mute = true; // Mute the audio instead of pausing it
            audioSource2.mute = true; // Mute the audio instead of pausing it
        }
    }
}
