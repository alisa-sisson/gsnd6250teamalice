using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    CharacterController characterController;
    float defaultHeight;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        defaultHeight = characterController.height;
    }

    private void Update()
    {
        if (Input.GetButton("Crouch"))
        {
            characterController.height *= 0.6f;
        }
        else
            characterController.height = defaultHeight;
    }
}
