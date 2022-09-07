using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // Game manager instance
    GameManager gameManager;

    // The text mesh to render the score in
    // Uses TextMeshProUGUI instead of TextMeshPro because otherwise it doesn't work for some reason
    TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        // Get game manager
        gameManager = GameManager.GetGameManager();

        // Get TextMeshPro component
        textMesh = GetComponent<TextMeshProUGUI>();
        Debug.Log(textMesh);

        // Create string of score
        string scoreText = $"Your Score Was: {gameManager.GetFormattedScore()}";
        Debug.Log($"ScoreText: {scoreText}");

        // Apply score text to TextMeshPro
        textMesh.text = scoreText;
    }
}
