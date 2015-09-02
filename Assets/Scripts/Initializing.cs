using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Initializing : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isServer)
            this.gameObject.AddComponent<Rigidbody>();

        Debug.Log(this.gameObject.GetComponent<Rigidbody>() != null);
	}
}
