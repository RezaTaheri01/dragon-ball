using System.Collections;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing. Please attach one to the GameObject.");
        }
    }

    // This function will be called when the button is clicked
    public void ExitGame()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            // Play the audio
            audioSource.Play();

            // Start the coroutine to wait for the audio to finish
            StartCoroutine(WaitForAudioToFinish());
        }
        else
        {
            // If no audio, quit immediately
            Quit();
        }
    }

    private IEnumerator WaitForAudioToFinish()
    {
        // Wait while the audio is playing
        while (audioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // Quit the game after the audio finishes
        Quit();
    }

    private void Quit()
    {
        #if UNITY_EDITOR
        // If running in the editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a built application, quit the application
        Application.Quit();
        #endif
    }
}
