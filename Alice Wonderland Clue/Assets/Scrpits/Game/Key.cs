using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

    public Sprite sprite;
    public string keyName;

    private void Start()
    {
        keyName = tag;
    }
}
