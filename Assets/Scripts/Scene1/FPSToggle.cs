using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Toggle

public class FPSToggle : MonoBehaviour
{

    // Reference to the Toggle (Checkbox)
    public Toggle FPS_Toggle;

    public TMPro.TextMeshProUGUI FPStext;

    private const string TogglePrefKey = "FPS_Toggle"; // Key for saving the toggle state
    // Start is called before the first frame update
    void Start()
    {
        if (FPS_Toggle != null){
            FPS_Toggle.onValueChanged.AddListener(OnToggleValueChanged);
            // Load the toggle state from PlayerPrefs and update the UI
            bool isToggleOn = PlayerPrefs.GetInt(TogglePrefKey, 1) == 1; // Default to true if not found
            FPS_Toggle.isOn = isToggleOn;

            // Ensure the audio state is correct based on the initial state of the checkbox
            UpdateFPSState(isToggleOn);
        }
    }

    // This method will be called whenever the checkbox (toggle) value changes
    private void OnToggleValueChanged(bool isChecked)
    {
        UpdateFPSState(isChecked);
        
        // Save the toggle state in PlayerPrefs
        PlayerPrefs.SetInt(TogglePrefKey, isChecked ? 1 : 0);
        PlayerPrefs.Save(); // Save changes immediately
    }


    // Method to update the audio playback based on the toggle state
    private void UpdateFPSState(bool isShowing)
    {
        if (isShowing)
        {
            FPStext.enabled = true;
        }
        else
        {
            FPStext.enabled = false;
        }
    }
}
