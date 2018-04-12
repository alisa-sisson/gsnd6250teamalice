using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIYPrompt : MonoBehaviour {

    bool displayed = false;

    public string CenterText = "There is a potion on the shelf!";
    public string Prompt = "Now you enter the other room.Use what you learned to get out of the room!";

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!displayed && other.CompareTag(Constants.Tags.Player))
        {
            GameManager.Instance.DisplayCenterText(CenterText);
            GameManager.Instance.DisplayPrompt(Prompt);
            displayed = true;
        }
    }
}
