using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLeaveTrigger : MonoBehaviour
{
    public Door relatedDoor;

    private void OnTriggerEnter(Collider other)
    {
        int currentLevel = GameManager.Instance.CurrentLevel;
        relatedDoor.locked = true;

        switch (currentLevel)
        {
            case 1:
                GameManager.Instance.DisplayCenterText("You forward into a tea room.");
                break;
            case 2:
                GameManager.Instance.DisplayCenterText("You proceed into a strange aisle.");
                break;
            default:
                break;
        }
        
    }
}
