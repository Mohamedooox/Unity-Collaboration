using UnityEngine;
using System.Collections.Generic;

public class AutoToggleEmission : MonoBehaviour
{
    public List<GameObject> objectsToToggle; // List of GameObjects to toggle emission
    private bool isEmissionOn = false;
    public float toggleInterval = 2f; // Time interval to toggle emission

    void Start()
    {
        // Start the toggling process
        InvokeRepeating("ToggleEmissionEffect", 0f, toggleInterval);
    }

    void ToggleEmissionEffect()
    {
        // Loop through each GameObject in the list
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null) // Check if the GameObject is not null
            {
                Renderer objectRenderer = obj.GetComponent<Renderer>();
                
                if (objectRenderer != null && objectRenderer.material.HasProperty("_EmissionColor"))
                {
                    // Toggle emission without changing color
                    if (isEmissionOn)
                    {
                        objectRenderer.material.EnableKeyword("_EMISSION");
                    }
                    else
                    {
                        objectRenderer.material.DisableKeyword("_EMISSION");
                    }
                }
            }
        }

        // Toggle the emission state
        isEmissionOn = !isEmissionOn;
    }
}
