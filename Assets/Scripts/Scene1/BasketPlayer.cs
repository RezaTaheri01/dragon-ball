using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Required for TextMesh Pro



public class BasketPlayer : MonoBehaviour
{

    public int player = 1;
    public TextMeshProUGUI score_player;
    public TextMeshProUGUI goalText;
    public float displayDuration = 2f; // How long the goal text should be displayed
    private init initScript; // Reference to the init script
    private int temp = 0;
    public AudioSource goalAudioSource;

    // This will be called when a goal is scored
    public void ShowGoalBanner()
    {
        // Play Goal sound
        if (goalAudioSource != null && goalAudioSource.clip != null)
        {
            goalAudioSource.PlayOneShot(goalAudioSource.clip);
        }
        else
        {
            Debug.LogWarning("Goal AudioSource or clip not assigned!");
        }
        
        // Set the text to show "Goal!"
        if (player == 1){
        goalText.text = "Dragon Goal!";
        }
        else{
        goalText.text = "Frog Goal!";
        }

        // Make the text visible
        goalText.gameObject.SetActive(true);

        // Start the coroutine to hide it after a delay
        StartCoroutine(HideGoalBanner());
    }
    void Start()
    {
        // Find the init script in the scene and store a reference to it
        initScript = FindObjectOfType<init>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that collided has the tag "ball"
        if (collision.gameObject.CompareTag("Ball"))
        {
            temp = int.Parse(score_player.text);
            score_player.text = $"{temp + 1}";
            initScript.reset();
            ShowGoalBanner();
        }
    }

    private IEnumerator HideGoalBanner()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the goal text
        goalText.gameObject.SetActive(false);
    }
    // void ResetGame()
    // {
    //     // Reload the current scene
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }
}