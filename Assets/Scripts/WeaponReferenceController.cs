using UnityEngine;
using System.Collections;

public class WeaponReferenceController : MonoBehaviour {
    public CameraController cameraController;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void LateUpdate () {
        this.transform.rotation = cameraController.camRot;
	}
}
