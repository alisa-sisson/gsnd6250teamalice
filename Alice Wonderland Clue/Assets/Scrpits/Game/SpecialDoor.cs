using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDoor : MonoBehaviour
{
    public float speedInterval = 0.01f;
    public bool opened = false;
    public bool locked = false;
    public bool keyArrived = false;
    public Sprite keySprite;

    public Alice.Size size = Alice.Size.Normal;

    public void Open()
    {
        // Check lock
        if (locked)
        {
            GameManager.Instance.DisplayCenterText("You need the key to unlock it.");
            return;
        }

        // Check size
        Alice.Size aliceSize = Alice.Instance.size;
        if (aliceSize != size)
        {
            if ((int)aliceSize > (int)size)
                GameManager.Instance.DisplayCenterText("You are too big for the door!\n" +
                    "Try to find a potion to change the size.");
            else
                GameManager.Instance.DisplayCenterText("The door seems too tall to reach!\n" +
                    "Try to find a potion to change the size.");
            return;
        }

        if (!opened)
        {
            StartCoroutine(RotateToCoroutine(90f));
            opened = true;
        }
    }

    public void Close()
    {
        if (opened)
        {
            StartCoroutine(RotateToCoroutine(0f));
            opened = false;
        }
    }

    IEnumerator RotateToCoroutine(float fromAngle, float toAngle)
    {
        Vector3 angles = transform.eulerAngles;
        angles.z = fromAngle;
        Quaternion from = Quaternion.Euler(angles);
        angles.z = toAngle;
        Quaternion to = Quaternion.Euler(angles);

        float progress = 0f;
        while (progress < 1f)
        {
            transform.rotation = Quaternion.Slerp(from, to, progress);
            progress += speedInterval;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator RotateToCoroutine(float toAngle)
    {
        Quaternion from = transform.rotation;
        Vector3 angles = transform.eulerAngles;
        angles.y = toAngle;
        Quaternion to = Quaternion.Euler(angles);

        float progress = 0f;
        while (progress < 1f)
        {
            transform.rotation = Quaternion.Slerp(from, to, progress);
            progress += speedInterval;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator AutoCloseCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (keyArrived)
            {
                if (!opened)
                {
                    Open();
                    GameManager.Instance.DisplayCenterText(string.Format("You opened {0} door using {0} key!", tag));
                    //GameManager.Instance.DisplayCenterImage(keySprite);
                    if (key) Destroy(key.gameObject);
                    GameManager.Instance.finalDoorsOpened += 1;
                }
            }
        }
    }

    Collider key;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            keyArrived = true;
            key = other;
        }
        else
        {
            if (!other.CompareTag(Constants.Tags.Player))
            {
                GameManager.Instance.DisplayCenterText(string.Format("This door requires a {0} key!", tag));
                //GameManager.Instance.DisplayCenterImage(keySprite);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        keyArrived = false;
    }

}
