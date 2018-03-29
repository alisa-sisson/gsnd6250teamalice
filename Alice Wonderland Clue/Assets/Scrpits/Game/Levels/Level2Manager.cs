using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : Singleton<Level2Manager>
{
    //public bool sphereCollected;
    //public bool cubeCollected;
    //public bool cylinderCollected;
    public int itemsCollected = 0;
    public GameObject growPotion;

    bool collectionCheck = true;

    private void Update()
    {
        if (collectionCheck)
        {
            //if (sphereCollected && cubeCollected && cylinderCollected)
            if (itemsCollected > 2)
            {
                // all collected, display portion.
                StartCoroutine(DisplayAllCollected(2.5f));
                collectionCheck = false;
            }
        }
    }

    IEnumerator DisplayAllCollected(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        growPotion.SetActive(true);
        GameManager.Instance.DisplayCenterText("All the items collected! Potion appears somewhere!");
    }
}
