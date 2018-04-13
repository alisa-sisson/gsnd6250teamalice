using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            if (other.GetComponent<Alice>().transform.localScale.x > 0.5f)
                GameManager.Instance.DisplayCenterText("This seems to be a door, but too small to get through.");
            else
                GameManager.Instance.DisplayCenterText("You beat the level!!!");
        }
    }
}
