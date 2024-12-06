using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // Set the starting time in seconds
    public TMP_Text timerText; // Reference to a TMP_Text component to display the time
    public string sceneToLoad = "NextScene"; // The name of the scene to load
    public GameObject monitoredObject; // The GameObject to monitor for activation
    public GameObject objectToUnhide; // The GameObject to unhide when the timer finishes
    public float delayBeforeSceneLoad = 2f; // Delay in seconds before loading the new scene

    private bool timerFinished = false; // Prevent multiple triggers of end logic

    void Update()
    {
        // Check if the monitored GameObject is active
        if (monitoredObject != null && monitoredObject.activeSelf)
        {
            return; // Exit Update early to stop the timer
        }

        if (!timerFinished && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // Decrease time by the time passed in each frame
            UpdateTimerText(); // Update the UI text with the remaining time
        }
        else if (!timerFinished)
        {
            timerFinished = true; // Mark the timer as finished
            timeRemaining = 0;
            UnhideObject(); // Unhide the specified GameObject
            StartCoroutine(DelayedSceneLoad()); // Start the delay coroutine
        }
    }

    void UpdateTimerText()
    {
        // Display the time remaining as minutes:seconds (e.g., 1:30)
        float minutes = Mathf.Floor(timeRemaining / 60);
        float seconds = timeRemaining % 60;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UnhideObject()
    {
        if (objectToUnhide != null)
        {
            objectToUnhide.SetActive(true); // Unhide the GameObject by activating it
        }
    }

    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad); // Wait for the specified delay
        SceneManager.LoadScene(sceneToLoad); // Load the specified scene
    }
}
