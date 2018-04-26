using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float speed = 0.1F;
    public bool opened = false;
    public bool locked = false;
    public bool atTheHandle = false;
    public static float autoCloseDuration = 3f;

    public void Open()
    {
        if (locked)
        {
            GameManager.Instance.DisplayCenterText("The door is locked.");
            return;
        }

        if (!opened)
        {
            StartCoroutine(RotateToCoroutine(90f));
            opened = true;
            StartCoroutine(AutoCloseCoroutine(autoCloseDuration));
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

    IEnumerator RotateToCoroutine(float toAngle)
    {
        Quaternion from = transform.rotation;
        Vector3 angles = transform.eulerAngles;
        angles.y = toAngle;
        Quaternion to = Quaternion.Euler(angles);

        while (Mathf.Abs(transform.eulerAngles.y - angles.y) > 0.001)
        {
            transform.rotation = Quaternion.Slerp(from, to, Time.time * speed);
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
            if (atTheHandle && !opened)
            {
                Open();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        atTheHandle = true;
        GameManager.Instance.DisplayPrompt("Press [E] to open the door.");
    }
    private void OnTriggerExit(Collider other)
    {
        atTheHandle = false;
    }

}
