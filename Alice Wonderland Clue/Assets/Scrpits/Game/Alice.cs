using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Alice : Singleton<Alice>
{
    public enum Size { Small = 0, Normal = 1, Big = 2 }

    public Size size = Size.Normal;
    //public List<string> Inventory = new List<string>();
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
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // Shrink potions
            InventoryItem item = Inventory.Instance.Use(Constants.Items.ShrinkPotion, true);
            if (item != null)
            {
                Size toSize = Size.Small;
                ChangeSize(size, toSize);
                GameManager.Instance.DisplayCenterText(string.Format("You used {0}!", item.name), 5f);
                //GameManager.Instance.HideItemAnim();
            }

        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            // Normal potions
            InventoryItem item = Inventory.Instance.Use(Constants.Items.NormalPotion, true);
            if (item != null)
            {
                Size toSize = Size.Normal;
                ChangeSize(size, toSize);
                GameManager.Instance.DisplayCenterText(string.Format("You used {0}!", item.name), 5f);
                //GameManager.Instance.HideItemAnim();
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            // Normal potions
            InventoryItem item = Inventory.Instance.Use(Constants.Items.GrowPotion, true);
            if (item != null)
            {
                Size toSize = Size.Big;
                ChangeSize(size, toSize);
                GameManager.Instance.DisplayCenterText(string.Format("You used {0}!", item.name), 5f);
                //GameManager.Instance.HideItemAnim();
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

    public void ChangeSize(Size from, Size to)
    {
        StartCoroutine(ChangeSizeCoroutine(from, to));
    }

    IEnumerator ChangeSizeCoroutine(Size from, Size to)
    {
        float oriSize;
        float oriWalkSpeed;
        float oriRunSpeed;
        float oriStepInterval;
        float newWalkSpeed;
        float newRunSpeed;
        float newStepInterval;
        float newSize;

        switch (from)
        {
            case Size.Big:
                oriSize = 8f;            // TODO:
                oriWalkSpeed = 20f;       // TODO:
                oriRunSpeed = 30f;       // TODO:
                oriStepInterval = 5f;    // TODO:
                break;
            case Size.Small:
                oriSize = 1.75f;
                oriWalkSpeed = 12f;
                oriRunSpeed = 7f;
                oriStepInterval = 7f;
                break;
            case Size.Normal:
                oriSize = 3.815913f;
                oriWalkSpeed = 15f;
                oriRunSpeed = 30f;
                oriStepInterval = 15f;
                break;
            default:
                yield break;
        }

        switch (to)
        {
            case Size.Big:
                newSize = 8f;            // TODO:
                newWalkSpeed = 20f;      // TODO:
                newRunSpeed = 30f;       // TODO:
                newStepInterval = 5f;    // TODO:
                break;
            case Size.Small:
                newSize = 1.75f;
                newWalkSpeed = 7f;
                newRunSpeed = 12f;
                newStepInterval = 7f;
                break;
            case Size.Normal:
                newSize = 3.815913f;
                newWalkSpeed = 15f;
                newRunSpeed = 30f;
                newStepInterval = 15f;
                break;
            default:
                yield break;
        }

        float progress = 0f;
        float shrinkStep = 0.01f;
        while (progress < 1f)
        {
            Vector3 newScale = Vector3.Lerp(new Vector3(oriSize, oriSize, oriSize), new Vector3(newSize, newSize, newSize), progress);
            gameObject.transform.localScale = newScale;
            float interWalkSpeed = Mathf.Lerp(oriWalkSpeed, newWalkSpeed, progress);
            float interRunSpeed = Mathf.Lerp(oriRunSpeed, newRunSpeed, progress);
            float interStepInterval = Mathf.Lerp(oriStepInterval, newStepInterval, progress);
            fps.WalkSpeed = interWalkSpeed;
            fps.RunSpeed = interRunSpeed;
            fps.StepInterval = interStepInterval;
            progress += shrinkStep;
            yield return new WaitForEndOfFrame();
        }

        size = to;
    }
}
