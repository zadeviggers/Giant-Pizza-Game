using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject[] layers;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    int counter = 0;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        foreach (GameObject obj in layers)
        {
            LoadChildObjects(obj);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        int childrenNeeded = Mathf.CeilToInt(screenBounds.x * 2 / objectWidth);

        // Create a clone to avoid messing with original
        GameObject clone = Instantiate(obj);

        for (int i = 0; i <= childrenNeeded; i++)
        {
            GameObject additionalClone = Instantiate(clone);
            additionalClone.transform.SetParent(obj.transform);
            // Use Vector3 here instead of Vector2 to preserve background layering
            additionalClone.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            additionalClone.name = obj.name + i;
        }

        // Destroy clone
        Destroy(clone);

        // Destory original SpriteRenderer
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void LateUpdate()
    {
        foreach(GameObject obj in layers)
        {
            RepositionChildObjects(obj);
        }
    }

    void RepositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth) {
                firstChild.transform.SetAsLastSibling();
                // Use Vector3 here instead of Vector2 to preserve background layering
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
                counter += 1;
                if (counter % 2 != 0) firstChild.transform.localScale = new Vector3(firstChild.transform.localScale.x * -1, firstChild.transform.localScale.y, firstChild.transform.localScale.z);


            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);


                counter += 1;
                if (counter % 2 != 0) lastChild.transform.localScale = new Vector3(lastChild.transform.localScale.x * -1, lastChild.transform.localScale.y, lastChild.transform.localScale.z);
            }
        }
    }
}
