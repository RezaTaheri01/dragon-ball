using UnityEngine;
using UnityEngine.UI;


public class BasketPlayer1 : MonoBehaviour
{

    public Text score_player_1;
    private int temp = 0;
    private init initScript; // Reference to the init script

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
            Debug.Log("Player 2 Goal!");
            temp = int.Parse(score_player_1.text);
            score_player_1.text = $"{temp + 1}";
            initScript.reset();
        }
    }

    // void ResetGame()
    // {
    //     // Reload the current scene
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }
}