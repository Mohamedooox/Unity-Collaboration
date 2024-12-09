using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI heartCounterText;
    public TextMeshProUGUI spunkiCounterText;
    public Button spunkiButton;
    public Button destroyHeartButton;
    public GameObject alternateGameObject;
    public GameObject objectToUnhide; // Reference to the GameObject to unhide
    public string nextSceneName;

    private int heartsCollected = 0;
    private int spunkiCounter = 0;
    private int totalHearts = 6;
    private Collider currentHeart;
    private Collider currentSpunki;
    private List<string> destroyedHeartNames = new List<string>();
    private List<GameObject> allSpunkiObjects;

    private void Start()
    {
        allSpunkiObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spunki"));

        spunkiButton.gameObject.SetActive(false);
        destroyHeartButton.gameObject.SetActive(false);
        alternateGameObject.SetActive(false);

        destroyHeartButton.onClick.AddListener(DestroyHeart);
        spunkiButton.onClick.AddListener(ActivateSpunkiAudio);

        // Initialize counters in the UI
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
            destroyedHeartNames.Add(currentHeart.gameObject.name);
            heartsCollected++;
            heartCounterText.text = $"{heartsCollected}/{totalHearts}";

            Destroy(currentHeart.gameObject);
            currentHeart = null;
            destroyHeartButton.gameObject.SetActive(false);
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

                    spunkiCounter++;
                    spunkiCounterText.text = $"{spunkiCounter}/{allSpunkiObjects.Count}";

                    if (spunkiRenderer != null)
{
    // Get all materials on the Renderer
    Material[] materials = spunkiRenderer.materials;

    // Loop through all materials
    foreach (Material material in materials)
    {
        // Check if the material has an "_Color" property (Albedo color)
        if (material.HasProperty("_Color"))
        {
            // Get the Albedo color of the material
            Color albedoColor = material.GetColor("_Color");

            // Enable emission and set its color to match the Albedo color
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", albedoColor);
        }
    }
}


                    spunkiButton.gameObject.SetActive(false);

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

            StartCoroutine(LoadNextSceneAfterDelay(2f));
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
