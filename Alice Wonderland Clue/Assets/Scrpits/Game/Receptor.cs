using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : MonoBehaviour
{

    public GameObject[] afters;
    bool itemEntered;
    Collider item;
    int currentRecept = 0;
    public string[] tagsInOrder;
    public GameObject growPotion;
    bool finishedCollecting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (finishedCollecting) return;
        string curTag = tagsInOrder[currentRecept];
        if (other.CompareTag(curTag))
        {
            GameManager.Instance.DisplayCenterText(string.Format("{0} seems a right fit!", other.gameObject.name));
            itemEntered = true;
            item = other;
        }
        else
        {
            GameManager.Instance.DisplayCenterText(string.Format("Doesn't seem right for {0}!\n Maybe there's an order.", other.gameObject.name));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        itemEntered = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemEntered)
            {
                string n = item.gameObject.name;
                GameManager.Instance.DisplayCenterText(string.Format("{0} is ready!", n));
                if (item) Destroy(item.gameObject);
                afters[currentRecept].gameObject.SetActive(true);
                //Level2Manager.Instance.itemsCollected += 1;
                currentRecept += 1;

                itemEntered = false;

                if (currentRecept >= 3)
                {
                    // Finished collecting
                    StartCoroutine(DisplayAllCollected(2.5f));
                    finishedCollecting = true;
                }
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
