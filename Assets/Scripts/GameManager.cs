using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
