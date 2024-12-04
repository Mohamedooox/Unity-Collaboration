using UnityEngine;
using UnityEngine.UI;

public class DoorOpenWithUIButton : MonoBehaviour
{
    public float rotationSpeed = 2f; // Speed of rotation
    public Button interactionButton; // Reference to the UI button

    private Transform currentDoor; // The door the player is interacting with
    private bool isInteracting = false; // Tracks whether the player is interacting with the door

    private void Start()
    {
        // Hide the button at the start
        interactionButton.gameObject.SetActive(false);

        // Add a listener to the button click event
        interactionButton.onClick.AddListener(ToggleDoor);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the tag "Door"
        if (other.CompareTag("Door"))
        {
            currentDoor = other.transform;

            // Show the interaction button
            interactionButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the button and clear the current door when the player leaves the trigger
        if (other.CompareTag("Door"))
        {
            currentDoor = null;
            interactionButton.gameObject.SetActive(false);
        }
    }

    private void ToggleDoor()
    {
        if (currentDoor != null)
        {
            // Get the current local rotation around the Y-axis
            float currentRotationY = currentDoor.localEulerAngles.y;

            // Allow for some tolerance around 0 and 90 degrees
            if (currentRotationY < 5f || currentRotationY > 355f) // Close to 0 degrees
            {
                // Rotate the door by +90 degrees if it's at or near 0 (in local space)
                StartCoroutine(RotateDoor(currentDoor, 90f));
            }
            else if (currentRotationY > 85f && currentRotationY < 95f) // Close to 90 degrees
            {
                // Rotate the door by -90 degrees if it's at or near 90 (in local space)
                StartCoroutine(RotateDoor(currentDoor, -90f));
            }
        }
    }

    private System.Collections.IEnumerator RotateDoor(Transform door, float targetRotation)
    {
        // Calculate the target local rotation (add the rotation to the current local Y-axis)
        Quaternion target = Quaternion.Euler(door.localEulerAngles.x, door.localEulerAngles.y + targetRotation, door.localEulerAngles.z);

        // Smoothly rotate the door towards the target local rotation
        while (Quaternion.Angle(door.localRotation, target) > 0.1f)
        {
            door.localRotation = Quaternion.Slerp(door.localRotation, target, Time.deltaTime * rotationSpeed);
            yield return null; // Wait for the next frame
        }

        // Ensure the final rotation matches exactly
        door.localRotation = target;
    }
}
