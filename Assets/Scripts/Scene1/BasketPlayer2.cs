using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Required for TextMesh Pro




public class BasketPlayer2 : MonoBehaviour
{
    public TextMeshProUGUI score_player_2;
    public TextMeshProUGUI goalText;
    public float displayDuration = 2f; // How long the goal text should be displayed
    private init initScript;


    // This will be called when a goal is scored
    public void ShowGoalBanner()
    {
        // Set the text to show "Goal!"
        goalText.text = "Frog Goal!";

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

    private int temp = 0;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that collided has the tag "ball"
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Player 1 Goal!");
            temp = int.Parse(score_player_2.text);
            score_player_2.text = $"{temp + 1}";
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