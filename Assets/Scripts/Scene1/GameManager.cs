using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMesh Pro
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Mode;
    public GameObject player;  // The player GameObject
    public GameObject mainCam;  // The player GameObject
    private PlayerMovement playerMovementScript;  // Reference to PlayerMovement2 script
    private AIPlayerMovement aiPlayerScript;  // Reference to AIPlayerMovement script
    private init Init;  // Reference to AIPlayerMovement script

    private bool isAIControlled = false;  // Flag to track control

    private void Start()
    {
        // Automatically get the scripts attached to the player GameObject
        playerMovementScript = player.GetComponent<PlayerMovement>();
        aiPlayerScript = player.GetComponent<AIPlayerMovement>();
        Init = mainCam.GetComponent<init>();

        // Initially, the player controls the character
        SwitchToPlayerControl();
        Mode.text = "Two Player";
        Init.reset();
    }

    public void ChangeMode()
    {

        if (isAIControlled)
        {
            SwitchToPlayerControl();
            Mode.text = "Two Player";
            EnableImage();
        }
        else
        {
            SwitchToAIControl();
            Mode.text = "Single Player";
            DisableImage();

        }
        Init.reset();

    }

    // Switch to AI control
    private void SwitchToAIControl()
    {
        aiPlayerScript.enabled = true;  // Enable AI control
        playerMovementScript.enabled = false;  // Disable player control
        isAIControlled = true;  // Set control flag to AI
    }

    // Switch to player control
    private void SwitchToPlayerControl()
    {
        aiPlayerScript.enabled = false;  // Disable AI control
        playerMovementScript.enabled = true;  // Enable player control
        isAIControlled = false;  // Set control flag to player
    }

    public void EnableImage()
    {
        if (image != null)
            image.enabled = true; // Enables the image
    }

    public void DisableImage()
    {
        if (image != null)
            image.enabled = false; // Disables the image
    }
}
