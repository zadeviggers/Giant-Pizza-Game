using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Starting,
    Menu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentState = GameState.Starting;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Check to make sure that this is the only instance of GameManager in the scene

        // All the GameManagers in the scene
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");

        // Variable for tracking if this is the only instace of GameManager in the scene
        bool notFirst = false;

        // Loop over all the game managers found above
        foreach (GameObject manager in gameManagers)
        {
            // Check that the object is from an assetbundle
            // scene.buildIndex is set to -1 when it is loaded from an asset bundle
            if (manager.scene.buildIndex == -1)
            {
                notFirst = true;
            }
        }

        // If this GameManager isn't the first in the scene, make it destroy itself
        // THERE CAN ONLY BE ONE!!
        if (notFirst == true) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);

        // If the level was loaded directly in the unity editor rather than going through the menu
        if(SceneManager.GetActiveScene().name.StartsWith("Level")) currentState = GameState.Playing;
        else currentState = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Playing)
        {
            score += 1;
        }

        Debug.Log($"Score: {score}, GameState: {currentState}");
    }

    // Static method for other scripts to load the GameManager instance
    public static GameManager GetGameManager()
    {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        GameManager gameManagerComponent = gameManagerObject.GetComponent<GameManager>();

        return gameManagerComponent;
    }

    public string GetFormattedScore()
    {
        return $"{score}";
    }

    public void GoToMenu() {
        Debug.Log("Loading menu");
        currentState = GameState.Menu;
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        Debug.Log("Starting game...");
        score = 0;
        currentState = GameState.Playing;
        SceneManager.LoadScene("Level 1");
    }

    public void Lose()
    {
        Debug.Log("Game lost");
        currentState = GameState.GameOver;
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
