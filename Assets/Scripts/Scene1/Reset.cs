using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMesh Pro



public class Reset : MonoBehaviour
{
    public GameObject mainCam;  // The player GameObject
    public TextMeshProUGUI score_player_1;
    public TextMeshProUGUI score_player_2;
    private init Init;


    private void start()
    {
        Init = mainCam.GetComponent<init>();
    }
    public void ResetGame()
    {
        // Reload the current active scene
        score_player_1.text = "0";
        score_player_2.text = "0";
        Init.reset();
    }

}
