using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    GameManager gameManager;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Only move while the game is being played
        if (gameManager.currentState != GameState.Playing) return;

        // Super scuffed way of making the ground look like it's moving
        // This will probably eventually cause a crash because the sprite gets too big
        sprite.size = new Vector2(sprite.size.x + MoveWithLevel.MoveAmount.x, sprite.size.y);
    }
}
