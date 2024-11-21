using UnityEngine;
using System.Collections;

public class init : MonoBehaviour
{
    public Transform ballStartPosition;     // Assign in Inspector
    public Transform player1StartPosition; // Assign in Inspector
    public Transform player2StartPosition; // Assign in Inspector

    public GameObject ballPrefab;     // Assign Prefab in Inspector
    public GameObject player1Prefab; // Assign Prefab in Inspector
    public GameObject player2Prefab; // Assign Prefab in Inspector

    private GameObject ballInstance;
    private GameObject player1Instance;
    private GameObject player2Instance;

    void Start()
    {
        ballInstance = Instantiate(ballPrefab);
        player1Instance = Instantiate(player1Prefab);
        player2Instance = Instantiate(player2Prefab);
    }

    public void reset()
    {
        // Stop animations in case reset is called again
        StopAllCoroutines();

        // Start animation to move objects to default positions
        StartCoroutine(MoveAndDestroyObjects(ballInstance, ballStartPosition.position));
        StartCoroutine(MoveAndDestroyObjects(player1Instance, player1StartPosition.position));
        StartCoroutine(MoveAndDestroyObjects(player2Instance, player2StartPosition.position));

        // Instantiate new objects and store references to them after the movements are done
        StartCoroutine(InstantiateAfterMovement());
    }

    private IEnumerator MoveAndDestroyObjects(GameObject instance, Vector3 targetPosition)
    {
        // Move to the target position with animation
        float duration = 1f; // Time in seconds
        float elapsedTime = 0f;
        Vector3 startPosition = instance.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            instance.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }
        if (instance != null)
        {
            instance.transform.position = targetPosition; // Snap to target position at the end
        }

        // // After movement is complete, destroy the object
        // Destroy(instance);
    }

    private IEnumerator InstantiateAfterMovement()
    {
        // Wait for the movement to complete (adjust the duration based on your movement time)
        yield return new WaitForSeconds(1f);

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
