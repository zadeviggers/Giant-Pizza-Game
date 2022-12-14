using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithLevel : MonoBehaviour
{
    public static Vector3 MoveAmount = new Vector3(0.1f, 0);
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        // Only move while the game is being played
        if (gameManager.currentState != GameState.Playing) return;

        // Use Vector3 instead of Vector2 to account for layering
        transform.position = new Vector3(transform.position.x + MoveAmount.x,  transform.position.y + MoveAmount.y, transform.position.z);
    }
}
