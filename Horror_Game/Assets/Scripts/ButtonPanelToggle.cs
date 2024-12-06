using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelToggle : MonoBehaviour
{
    public Button firstButton; // Reference to the first button
    public Button secondButton; // Reference to the second button
    public GameObject panel; // Reference to the panel to activate/deactivate

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the panel is deactivated at the start
        panel.SetActive(false);

        // Add listeners to buttons
        firstButton.onClick.AddListener(OnFirstButtonClick);
        secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    // When the first button is clicked
    void OnFirstButtonClick()
    {
        firstButton.gameObject.SetActive(false); // Deactivate the first button
        panel.SetActive(true); // Activate the panel
    }

    // When the second button is clicked
    void OnSecondButtonClick()
    {
        panel.SetActive(false); // Deactivate the panel
        firstButton.gameObject.SetActive(true); // Reactivate the first button
    }
}
