using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Required for TextMesh Pro
using UnityEngine.SceneManagement;




public class BasketPlayerBoss : MonoBehaviour
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

    private TextMeshProUGUI bossName; // Reference to the TMP text
    public float fadeDuration = 2.0f; // Duration for fading in and out
    // public float displayDuration = 3.0f; // Duration to display the text before fading out

    private Coroutine fadeCoroutine;
    private int winScore;

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
        goalText.text = "Dragon Goal!!!";
        }
        else{
        goalText.text = "Evil Frog Goal";
        }

        // Make the text visible
        goalText.gameObject.SetActive(true);

        // Start the coroutine to hide it after a delay
        StartCoroutine(HideGoalBanner());
    }
    void Start()
    {
        bossName = GameObject.Find("BossNameText")?.GetComponent<TextMeshProUGUI>();
        bossName.color = Color.red;
        ShowBossNameAndTransition("EVIL FROG", -1);

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
            winScore = initScript.get_win_score();
            if (temp == winScore){
                // show a banner that say someone ready for boss and wait a few second
                // or voice :)
                if (player==1){
                    bossName.color = Color.green;
                    ShowBossNameAndTransition("YOU WON", 1);
                }else{
                    bossName.color = Color.red;
                    ShowBossNameAndTransition("YOU LOSE", -1);
                    score_player.text = "0";
                    score_enemy.text = "0";
                }
            }
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
            initScript.add_win_score();
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
        // Call this to show the boss name with a fade-in effect
    public void ShowBossNameAndTransition(string text, int sceneIndex)
    {
        // Dynamically find the bossName TextMeshPro element
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        bossName.text = text;
        bossName.gameObject.SetActive(true);

        // Start the fade-in coroutine
        fadeCoroutine = StartCoroutine(FadeTextAlpha(0, 1, fadeDuration, () =>
        {
            StartCoroutine(HideStatusBannerAndTransition(sceneIndex)); // Automatically hide after displayDuration and change scene
        }));
    }

    private IEnumerator HideStatusBannerAndTransition(int sceneIndex)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Start the fade-out coroutine
        fadeCoroutine = StartCoroutine(FadeTextAlpha(1, 0, fadeDuration, () =>
        {
            if (sceneIndex != -1){
                SceneManager.LoadScene(sceneIndex); // Transition to the specified scene
            }
            bossName.gameObject.SetActive(false); // Disable the text after fading out
        }));
    }

    private IEnumerator FadeTextAlpha(float startAlpha, float endAlpha, float duration, System.Action onComplete = null)
    {
        float elapsedTime = 0;
        Color originalColor = bossName.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            bossName.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        bossName.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);

        onComplete?.Invoke();
    }
}