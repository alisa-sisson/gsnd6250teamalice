using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Alice : MonoBehaviour
{

    public List<string> Inventory = new List<string>();

    [SerializeField]
    float pickupDistance = 1.5f;

    FirstPersonController fps;

    public float PickupDistance
    {
        get
        {
            return pickupDistance;
        }

        set
        {
            pickupDistance = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        fps = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (Inventory.Count > 0)
            {
                if (Inventory[0] == Constants.Items.ShrinkPotion)
                {
                    Shrink();
                    GameManager.Instance.DisplayCenterText(string.Format("You used {0}!", Inventory[0]));
                    Inventory.RemoveAt(0);
                    GameManager.Instance.HideItemAnim();
                }
            }

        }
    }

    public void Shrink()
    {
        StartCoroutine(ShrinkCoroutine());
    }

    IEnumerator ShrinkCoroutine()
    {
        float progress = 0f;
        float shrinkStep = 0.01f;
        while (progress < 1f)
        {
            Vector3 newScale = Vector3.Lerp(Vector3.one, new Vector3(0.1f, 0.1f, 0.1f), progress + shrinkStep);
            gameObject.transform.localScale = newScale;
            float newWalkSpeed = Mathf.Lerp(5f, 1f, progress + shrinkStep);
            float newRunSpeed = Mathf.Lerp(10f, 2f, progress + shrinkStep);
            float newStepInterval = Mathf.Lerp(5f, 1f, progress + shrinkStep);
            fps.WalkSpeed = newWalkSpeed;
            fps.RunSpeed = newRunSpeed;
            fps.StepInterval = newStepInterval;
            progress += shrinkStep;
            yield return new WaitForEndOfFrame();
        }
    }
}
