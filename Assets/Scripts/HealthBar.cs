using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Heart sprites
    public Image EmptyHeart;
    public Image HalfHeart;
    public Image FullHeart;

    public float imageSize;

    // Components
    Canvas canvas;

    // Player
    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Remove old hearts
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Maths for how many hearts of each type are needed
        float numberOfHearts = Mathf.Ceil(player.maxHealth / 2);

        bool needHalfHeart = player.health % 2 != 0;

        float fullHearts = Mathf.Floor(player.health / 2);


        // Create new children
        for (int i = 1; i <= numberOfHearts; i++)
        {
            Vector2 position = new Vector2(i * imageSize + player.transform.position.x, imageSize + player.transform.position.y);

            Image newChild;
            if (i <= fullHearts)
            {
                // Add a full heart
                newChild = Instantiate(FullHeart);
            }
            else if (i == fullHearts + 1 && needHalfHeart)
            {
                // Add a half heart
                newChild = Instantiate(HalfHeart);
            }
            else
            {
                // Add an empty heart
                newChild = Instantiate(EmptyHeart);
            }
            newChild.transform.SetParent(transform);
            newChild.transform.position = position;
        }
    }
}
