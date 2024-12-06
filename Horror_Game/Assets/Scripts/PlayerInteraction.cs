using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI heartCounterText; // UI text for hearts collected
    public TextMeshProUGUI spunkiCounterText; // UI text for Spunki activated
    public Button spunkiButton; // Button to activate Spunki
    public Button destroyHeartButton; // Button to destroy hearts
    public GameObject alternateGameObject; // Alternate object shown when no match is found
    public string nextSceneName; // Name of the next scene
	public GameObject objectToUnhide; // Reference to the GameObject to unhide

    private int heartsCollected = 0; // Counter for hearts collected
    private int spunkiCounter = 0; // Counter for Spunki activated
    private int totalHearts = 20; // Total hearts to collect
    private Collider currentHeart; // Reference to the currently interacted Heart
    private Collider currentSpunki; // Reference to the currently interacted Spunki
    private List<string> destroyedHeartNames = new List<string>(); // List of destroyed heart names
    private List<GameObject> allSpunkiObjects; // List of all Spunki objects

    private void Start()
    {
        // Initialize Spunki list
        allSpunkiObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spunki"));

        spunkiButton.gameObject.SetActive(false);
        destroyHeartButton.gameObject.SetActive(false);
        alternateGameObject.SetActive(false);

        destroyHeartButton.onClick.AddListener(DestroyHeart);
        spunkiButton.onClick.AddListener(ActivateSpunkiAudio);

        // Initialize UI
        heartCounterText.text = $"{heartsCollected}/{totalHearts}";
        spunkiCounterText.text = $"{spunkiCounter}/{allSpunkiObjects.Count}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            currentHeart = other; 
            destroyHeartButton.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Spunki"))
        {
            currentSpunki = other;

            if (destroyedHeartNames.Contains(other.gameObject.name))
            {
                spunkiButton.gameObject.SetActive(true);
                alternateGameObject.SetActive(false);
            }
            else
            {
                alternateGameObject.SetActive(true);
                spunkiButton.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Heart") && currentHeart == other)
        {
            currentHeart = null;
            destroyHeartButton.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Spunki") && currentSpunki == other)
        {
            currentSpunki = null;
            spunkiButton.gameObject.SetActive(false);
            alternateGameObject.SetActive(false);
        }
    }

    private void DestroyHeart()
    {
        if (currentHeart != null)
        {
            destroyedHeartNames.Add(currentHeart.gameObject.name); // Save the destroyed heart's name
            heartsCollected++; // Increment the heart counter

            // Update the UI to display hearts collected in the format "1/20"
            heartCounterText.text = $"{heartsCollected}/{totalHearts}";

            Destroy(currentHeart.gameObject); // Destroy the Heart GameObject
            currentHeart = null; // Clear the current Heart reference

            destroyHeartButton.gameObject.SetActive(false); // Hide the destroy button
        }
    }

    private void ActivateSpunkiAudio()
    {
        if (currentSpunki != null)
        {
            if (destroyedHeartNames.Contains(currentSpunki.gameObject.name))
            {
                AudioSource audioSource = currentSpunki.GetComponent<AudioSource>();
                Renderer spunkiRenderer = currentSpunki.GetComponent<Renderer>();

                if (audioSource != null && !audioSource.enabled)
                {
                    audioSource.enabled = true;

                    spunkiCounter++; // Increment the Spunki counter
                    spunkiCounterText.text = $"{spunkiCounter}/{allSpunkiObjects.Count}";

                    if (spunkiRenderer != null && spunkiRenderer.material != null)
                    {
                        Material material = spunkiRenderer.material;
                        material.EnableKeyword("_EMISSION");
                        material.SetColor("_EmissionColor", Color.yellow * 2.0f);
                    }

                    spunkiButton.gameObject.SetActive(false);

                    // Check if all Spunki objects have activated their audio
                    CheckAllSpunkiActivated();
                }
            }
        }
    }

    private void CheckAllSpunkiActivated()
{
    if (spunkiCounter == allSpunkiObjects.Count)
    {
        // Unhide the specified GameObject
        if (objectToUnhide != null)
        {
            objectToUnhide.SetActive(true);
        }

        StartCoroutine(LoadNextSceneAfterDelay(4f)); // Load the next scene after a short delay
    }
}

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
