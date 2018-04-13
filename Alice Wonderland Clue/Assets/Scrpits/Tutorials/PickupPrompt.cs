using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPrompt : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DisplayPickupPrompt();
            
        }
    }
}
