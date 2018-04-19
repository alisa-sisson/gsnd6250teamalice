using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	// plays 1 instance of background music, continues through each scene

	void Awake () {

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Music");
		if (objs.Length > 1)
			Destroy (this.gameObject);
		
		DontDestroyOnLoad (this.gameObject);
	}

}
