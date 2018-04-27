using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnterTrigger : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        int currentLevel = GameManager.Instance.CurrentLevel;

        switch (currentLevel)
        {
            case 1:
                // Enters level 1
                GameManager.Instance.doorLv1.locked = true;
                break;
            default:
                break;
        }
    }
}
