using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    Alice player;
    Transform playerHand;
    public bool Pickedup { get; set; }

    private void Start()
    {
        playerHand = GameObject.FindGameObjectWithTag(Constants.Tags.PlayerHand).transform;
        player = playerHand.transform.parent.GetComponent<Alice>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Pickedup)
            {
                float dist = Vector3.Distance(player.gameObject.transform.position, transform.position);
                if (dist > player.PickupDistance) return;

                GetComponent<Rigidbody>().isKinematic = true;
                transform.position = playerHand.position;
                transform.parent = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
                Pickedup = true;
            }
            else
            {
                transform.parent = GameObject.FindGameObjectWithTag(Constants.Tags.Stuff).transform;
                GetComponent<Rigidbody>().isKinematic = false;
                Pickedup = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            GameManager.Instance.DisplayPrompt("Press E to pickup the item.");
        }
    }
}
