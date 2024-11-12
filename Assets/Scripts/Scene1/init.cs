using UnityEngine;

public class init : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private GameObject ballInstance;
    private GameObject player1Instance;
    private GameObject player2Instance;

    void Start()
    {
        reset();
    }
    

    public void reset()
    {
        // Remove previous instances if they exist
        if (ballInstance != null) Destroy(ballInstance);
        if (player1Instance != null) Destroy(player1Instance);
        if (player2Instance != null) Destroy(player2Instance);

        // Instantiate new objects and store references to them
        ballInstance = Instantiate(ballPrefab);
        player1Instance = Instantiate(player1Prefab);
        player2Instance = Instantiate(player2Prefab);
    }
}
