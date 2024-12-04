using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera camera; // Assign the camera in the Inspector
    public float shake = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPosition;

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main; // Default to the main camera if not set
        }
        originalPosition = camera.transform.localPosition;
    }

    void Update()
    {
        if (shake > 0)
        {
            camera.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
            camera.transform.localPosition = originalPosition; // Reset to the original position
        }
    }

    // Call this function to start the shake
    public void TriggerShake(float duration)
    {
        shake = duration;
    }
}
