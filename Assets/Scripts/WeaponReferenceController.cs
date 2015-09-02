using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponReferenceController : NetworkBehaviour {
    
    public CameraController cameraController;
    public PlayerController playerController;
    public WeaponController weaponController;
    public GameObject thisWeaponReference;

    Vector3 weapPosFP;
    Vector3 weapPosTP;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer) return;
        weapPosTP = Vector3.Scale(thisWeaponReference.transform.localPosition, new Vector3(0, 1, 0));
        weapPosFP = thisWeaponReference.transform.localPosition;

	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!isLocalPlayer) return;

        if (cameraController.controlMode == 0)
        {
            thisWeaponReference.transform.rotation = cameraController.camRot;
            thisWeaponReference.transform.localPosition = weapPosFP;

        }
        else if (cameraController.controlMode == 1 && playerController.moving == false && weaponController.shooting == false)
        {
            thisWeaponReference.transform.rotation = Quaternion.Euler(Vector3.Scale(playerController.transform.localRotation.eulerAngles, new Vector3(0, 1, 0)) + Vector3.Scale(cameraController.camRot.eulerAngles, new Vector3(1, 0, 1)));
            thisWeaponReference.transform.localPosition = weapPosTP;
        }
        else if (cameraController.controlMode == 1 && (playerController.moving == true || weaponController.shooting == true))
        {
            thisWeaponReference.transform.rotation = Quaternion.Euler(playerController.transform.localRotation.eulerAngles + Vector3.Scale(cameraController.camRot.eulerAngles, new Vector3(1, 0, 1)));
            thisWeaponReference.transform.localPosition = weapPosTP;
            weaponController.shooting = false;
        }

	}
}
