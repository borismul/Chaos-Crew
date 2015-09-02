using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetController : NetworkBehaviour {
    public bool localPlayer;
	// Use this for initialization
	void Start () {
        localPlayer = isLocalPlayer;
	}
	
}
