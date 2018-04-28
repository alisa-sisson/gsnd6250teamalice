using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

    [SerializeField] Sprite potionUI;

    private void OnTriggerEnter(Collider other)
    {
        PickUpPotion(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PickUpPotion(collision.collider);
    }

    void PickUpPotion(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            string potionName = gameObject.name;
            GameManager.Instance.DisplayCenterText(string.Format("You got a {0}", potionName));
            string key = "[1]";
            if (potionName.Equals(Constants.Items.ShrinkPotion))
                key = "[1]";
            else if (potionName.Equals(Constants.Items.NormalPotion))
                key = "[2]";
            else
                key = "[3]";
            GameManager.Instance.DisplayPrompt(string.Format("Press {0} to use the potion.", key));
            Inventory.Instance.Add(potionName, potionUI, true);

            //other.GetComponent<Alice>().Inventory.Add(potionName);
            Destroy(gameObject);
        }
    }
}
