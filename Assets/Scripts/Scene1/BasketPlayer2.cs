using UnityEngine;
using UnityEngine.UI;



public class BasketPlayer2 : MonoBehaviour
{
    public Text score_player_2;
    private init initScript;

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
        }
    }


    // void ResetGame()
    // {
    //     // Reload the current scene
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

}