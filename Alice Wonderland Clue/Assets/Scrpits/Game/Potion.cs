using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

    [SerializeField] Sprite potionUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            string potionName = gameObject.name;
            GameManager.Instance.DisplayCenterText(string.Format("You got a {0}", potionName));
            GameManager.Instance.DisplayPrompt("Press [R] to use the potion.");
            GameManager.Instance.DisplayItemAnim();
            other.GetComponent<Alice>().Inventory.Add(potionName);
            Destroy(gameObject);
        }
    }
}
