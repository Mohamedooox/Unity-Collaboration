using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject objectToDeactivate; // Reference to the GameObject to deactivate

    public void Pause()
    {
        // Deactivate all components of the GameObject
        if (objectToDeactivate != null)
        {
            foreach (MonoBehaviour component in objectToDeactivate.GetComponents<MonoBehaviour>())
            {
                component.enabled = false; // Disable all MonoBehaviour components
            }
        }
        
        Time.timeScale = 0f; // Pause the game
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Resume the game
        
        // Reactivate all components of the GameObject
        if (objectToDeactivate != null)
        {
            foreach (MonoBehaviour component in objectToDeactivate.GetComponents<MonoBehaviour>())
            {
                component.enabled = true; // Enable all MonoBehaviour components
            }
        }
    }
}
