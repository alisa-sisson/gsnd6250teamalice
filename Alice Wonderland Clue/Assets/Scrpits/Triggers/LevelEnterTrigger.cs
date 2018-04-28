using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnterTrigger : MonoBehaviour
{
    public Door relatedDoor;
    public Door relatedDoorExtra;

    private void OnTriggerEnter(Collider other)
    {
        int currentLevel = GameManager.Instance.CurrentLevel;

        switch (currentLevel)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                relatedDoor.locked = false;
                if (relatedDoorExtra != null) relatedDoorExtra.locked = false;
                break;
            default:
                break;
        }
    }
}
