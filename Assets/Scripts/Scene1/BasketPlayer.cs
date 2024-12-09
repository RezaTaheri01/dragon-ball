using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Required for TextMesh Pro
using UnityEngine.SceneManagement;




public class BasketPlayer : MonoBehaviour
{

    public int player = 1;
    public TextMeshProUGUI score_player;
    public TextMeshProUGUI score_enemy;
    public TextMeshProUGUI goalText;
    public float displayDuration = 2f; // How long the goal text should be displayed
    private init initScript; // Reference to the init script
    private int temp = 0;
    private int temp2 = 0;
    public AudioSource goalAudioSource;
    public AudioSource persuasionAudioSource;
    public AudioSource drawAudioSource;
    private int base_diff = 2;
    private int diff = 2;


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
        goalText.text = "Frog Goal!!!";
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
            temp2 = int.Parse(score_enemy.text);
            score_player.text = $"{temp + 1}";
            // if (temp == 20 || temp2 == 20){
            //     // show a banner that say someone ready for boss and wait a few second
            //     // or voice :)
            //     SceneManager.LoadScene(2);
            // }
            PlaySound(temp, temp2);
            initScript.reset();
            ShowGoalBanner();
        }
    }

    private void PlaySound(int temp, int temp2){
        if (temp+1==temp2){
            diff = base_diff;
            // play draw sound
            if (drawAudioSource != null && drawAudioSource.clip != null)
            {
                drawAudioSource.PlayOneShot(drawAudioSource.clip);
            }
        }else if(temp - temp2 == diff){
            diff += base_diff;
            if (persuasionAudioSource != null && persuasionAudioSource.clip != null)
            {
                persuasionAudioSource.PlayOneShot(persuasionAudioSource.clip);
            }
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