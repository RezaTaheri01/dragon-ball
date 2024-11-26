using UnityEngine;

public class Pause : MonoBehaviour
{
    // private bool isPaused = false;
    private init initScript; // Reference to the Init script

    public GameObject menuPanel;

    void Start()
    {
        // Find the Init script in the scene and store a reference to it
        initScript = FindObjectOfType<init>();

        // Make sure the menu panel is hidden at the start
        Resume();
    }

    void Update()
    {
        // Check if the space key is pressed
        if (menuPanel.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(Time.timeScale == 0){
                    Resume();
                }
                menuPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                initScript.reset();
            }
        }else if (Input.GetKeyDown(KeyCode.Space) &&  Time.timeScale == 0){
                    Resume();
        }
    }

    // void TogglePause()
    // {
    //     // Toggle the pause state
    //     isPaused = !isPaused;

    //     // If paused, set time scale to 0; if unpaused, set it to 1
    //     Time.timeScale = isPaused ? 0 : 1;
    // }

    // void ToggleMenu()
    // {
    //     // Toggle the menu panel's active state
    //     menuPanel.SetActive(!menuPanel.activeSelf);
    // }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
