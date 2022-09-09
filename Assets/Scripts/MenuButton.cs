using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    // Game manager instance
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the game manager instance
        gameManager = GameManager.GetGameManager();
    }

    // Called when the button is pressed
    private void OnMouseUpAsButton()
    {
        Debug.Log("Clicked!");
        // Go to the menu on click
        gameManager.GoToMenu();
    }
}
