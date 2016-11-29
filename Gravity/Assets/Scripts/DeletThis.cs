using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeletThis : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            clearListOfBlackHoles();
        }
    }

    void clearListOfBlackHoles()
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
