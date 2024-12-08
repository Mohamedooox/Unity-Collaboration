using UnityEngine;

public class DebugTriggerInteraction : MonoBehaviour
{
    // Public references for the Animator, the Canvas components, and the Camera components
    public Canvas canvasToDeactivate;
    public Canvas canvasToActivate;
    public Camera cameraToDeactivate;
    public Camera cameraToActivate;
    public AudioSource audioSourceToDeactivate; // Reference to the AudioSource to deactivate

    private void OnTriggerEnter(Collider other)
    {
        // Check if the interacting object has the tag "Player" and this object has the tag "Enemy"
        if (other.CompareTag("Player") && gameObject.CompareTag("Enemy"))
        {
            // Deactivate the first Canvas and activate the second one
            if (canvasToDeactivate != null)
            {
                canvasToDeactivate.gameObject.SetActive(false);
            }

            if (canvasToActivate != null)
            {
                canvasToActivate.gameObject.SetActive(true);
            }

            // Deactivate the first Camera and activate the second one
            if (cameraToDeactivate != null)
            {
                cameraToDeactivate.gameObject.SetActive(false);
            }

            if (cameraToActivate != null)
            {
                cameraToActivate.gameObject.SetActive(true);
            }

            // Deactivate the specified AudioSource
            if (audioSourceToDeactivate != null)
            {
                audioSourceToDeactivate.enabled = false;
            }
        }
    }
}
