using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeletThis : MonoBehaviour
{
    void OnMouseOver()
    {
        // Right click or middle click on black hole to delete it
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            clearListOfBlackHoles();
        }
    }

    // Get rid of deleted black holes
    public void clearListOfBlackHoles()
    {
        List<GameObject> removeTheseFromList = new List<GameObject>();   
        foreach (GameObject blackHole in GameManager.blackHoles)
        {
            if (!blackHole.activeSelf)
                removeTheseFromList.Add(blackHole);
        }
        foreach (GameObject removeThis in removeTheseFromList)
        {
            GameManager.blackHoles.Remove(removeThis);
        }
    }
}
