using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerProxy : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
    }

    public void GoToMenu()
    {
        gameManager.GoToMenu();
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }
}
