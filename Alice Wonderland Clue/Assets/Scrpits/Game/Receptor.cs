using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : MonoBehaviour
{

    public GameObject after;
    public bool Triggered;
    bool itemEntered;
    Collider item;

    private void OnTriggerEnter(Collider other)
    {
        string ownTag = tag;
        if (other.CompareTag(tag))
        {
            GameManager.Instance.DisplayCenterText(string.Format("Put the {0} at the {0} receptacle!", other.gameObject.name));
            itemEntered = true;
            item = other;
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
                after.gameObject.SetActive(true);
                Level2Manager.Instance.itemsCollected += 1;
                itemEntered = false;
            }
        }
    }
}
