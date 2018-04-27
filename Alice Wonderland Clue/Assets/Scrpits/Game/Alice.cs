using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Alice : MonoBehaviour
{

    public List<string> Inventory = new List<string>();
    //Transform playerHand;

    Pickable pickedUpObj;

    [SerializeField]
    float pickupDistance = 1.5f;

    [SerializeField]
    float carryDistance = 1f;

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

    public bool Carrying
    {
        get { return pickedUpObj != null; }
    }

    // Use this for initialization
    void Start()
    {
        fps = GetComponent<FirstPersonController>();
        //playerHand = GameObject.FindGameObjectWithTag(Constants.Tags.PlayerHand).transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        if (Carrying)
        {
            if (pickedUpObj.Touched)
            {
                PutDownObj();
            }
            else
            {
                float cdis = carryDistance;
                //RaycastHit hit;
                //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                //{
                //    cdis = Mathf.Min(hit.distance, carryDistance);
                //}

                pickedUpObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward * cdis;
            }
        }
    }

    private void CheckInput()
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Have I picked up one thing already?
            if (Carrying)
            {
                // Put it down
                PutDownObj();
            }
            else
            {
                // Cast a ray
                RaycastHit hit;
                LayerMask layerMask = LayerMask.GetMask("Pickable");
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, layerMask))
                {
                    Pickable o = hit.collider.GetComponent<Pickable>();
                    if (o != null)
                    {
                        float dist = Vector3.Distance(transform.position, o.gameObject.transform.position);
                        if (dist > PickupDistance) return;

                        // Ok pick it up
                        pickedUpObj = o.GetComponent<Pickable>();
                        pickedUpObj.GetComponent<Rigidbody>().isKinematic = true;
                        pickedUpObj.transform.parent = transform;
                        //pickedUpObj.transform.position = playerHand.position;
                    }
                }
            }
        }
    }

    private void PutDownObj()
    {
        pickedUpObj.transform.parent = GameObject.FindGameObjectWithTag(Constants.Tags.Stuff).transform;
        pickedUpObj.GetComponent<Rigidbody>().isKinematic = false;
        pickedUpObj = null;
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
            Vector3 newScale = Vector3.Lerp(Vector3.one, new Vector3(0.2f, 0.2f, 0.2f), progress + shrinkStep);
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
