using UnityEngine;
using System.Collections;

public class HideAfterTime : MonoBehaviour
{
    public GameObject objectToHide; // Assign the GameObject in the Inspector
    public float delay = 5f; // Time in seconds before hiding the object

    void Start()
    {
        StartCoroutine(HideObjectAfterDelay());
    }

    IEnumerator HideObjectAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        objectToHide.SetActive(false); // Hide the object
    }
}