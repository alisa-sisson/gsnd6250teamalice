using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float speed = 0.1F;
    public bool opened = false;

    public void Open()
    {
        StartCoroutine(OpenCoroutine());
    }

    IEnumerator OpenCoroutine()
    {
        //Quaternion from = transform.rotation;
        //Vector3 angles = transform.eulerAngles;
        //angles.x = -25f;
        //Quaternion to = Quaternion.Euler(new Vector3(0, 90, 90));

        //while (transform.eulerAngles.x - angles.x > 0.001)
        //{
        //    transform.rotation = Quaternion.Slerp(from, to, Time.time * speed);
        //    yield return new WaitForEndOfFrame();
        //}

        Vector3 from = transform.eulerAngles;
        Vector3 to = new Vector3(0, 90, 90);
        while(transform.eulerAngles.x - to.x > 0.001)
        {
            transform.Rotate(Vector3.up * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!opened)
            {
                Open();
            }
            opened = !opened;
        }
    }
}
