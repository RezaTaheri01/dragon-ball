using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void ExitGame()
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
