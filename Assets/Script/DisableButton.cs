using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public GameObject Character;
    public Button switchButton; // Reference to the switch character button
    public Sprite greySprite; // The greyed-out sprite or image

    // Call this method to disable the switch button and change the image to the greyed-out sprite
    public void UnButton()
    {
            switchButton.interactable = false; // Disable the button
            Image buttonImage = switchButton.GetComponent<Image>(); // Get the image component of the button
            Debug.Log("Character is null. Disabling the button.");
           
        
        if (buttonImage != null && greySprite != null)
            {
                buttonImage.sprite = greySprite; // Set the button's image to the greyed-out sprite
            }

    }
}
