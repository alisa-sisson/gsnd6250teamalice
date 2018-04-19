using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    //Alice player;
    //Transform playerHand;
    public bool Pickedup { get; set; }
    public bool Touched { get; set; }

    private void Start()
    {

    }

    public void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            GameManager.Instance.DisplayPrompt("Press E to pickup the item.");
        }

        // I touched something
        if (Pickedup)
            Touched = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (Pickedup)
            Touched = false;
    }
}
