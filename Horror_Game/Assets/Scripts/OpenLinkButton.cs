using UnityEngine;

public class OpenLinkButton : MonoBehaviour
{
    // URL to open
    public string url = "https://example.com";

    // Method to be called when the button is clicked
    public void OpenLink()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogError("URL is not set!");
        }
    }
}
