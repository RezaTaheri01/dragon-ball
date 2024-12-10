using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomName : MonoBehaviour
{

    public TextMeshProUGUI roomName; // Reference to the TMP text
    public string text="Playground";
    public float fadeDuration = 2.0f; // Duration for fading in and out
    public float displayDuration = 2.0f; // Duration to display the text before fading out

    private Coroutine fadeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
      ShowRoomNameAndTransition();  
    }

        // Call this to show the boss name with a fade-in effect
    public void ShowRoomNameAndTransition()
    {
        // Dynamically find the RoomName TextMeshPro element
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        roomName.text = text;
        roomName.gameObject.SetActive(true);

        // Start the fade-in coroutine
        fadeCoroutine = StartCoroutine(FadeTextAlpha(0, 1, fadeDuration, () =>
        {
            StartCoroutine(HideStatusBannerAndTransition()); // Automatically hide after displayDuration and change scene
        }));
    }

    private IEnumerator HideStatusBannerAndTransition()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Start the fade-out coroutine
        fadeCoroutine = StartCoroutine(FadeTextAlpha(1, 0, fadeDuration, () =>
        {
        }));
    }

    private IEnumerator FadeTextAlpha(float startAlpha, float endAlpha, float duration, System.Action onComplete = null)
    {
        float elapsedTime = 0;
        Color originalColor = roomName.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            roomName.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        roomName.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);

        onComplete?.Invoke();
    }
}
