using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float speed = 0.1F;
    public bool opened = false;
    public bool atTheHandle = false;

    public void Open()
    {
        if (!opened)
            StartCoroutine(RotateToCoroutine(90f));
    }

    public void Close()
    {
        if (opened)
            StartCoroutine(RotateToCoroutine(0f));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (atTheHandle && !opened)
            {
                Open();
                opened = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        atTheHandle = true;
    }
    private void OnTriggerExit(Collider other)
    {
        atTheHandle = false;
    }

}
