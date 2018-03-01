using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIYPrompt : MonoBehaviour {

    bool displayed = false;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!displayed && other.CompareTag(Constants.Tags.Player))
        {
            GameManager.Instance.DisplayCenterText("There is a potion on the shelf!");
            GameManager.Instance.DisplayPrompt("Now you enter the other room. Use what you learned to get out of the room!");
            displayed = true;
        }
    }
}
