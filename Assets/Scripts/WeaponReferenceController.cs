using UnityEngine;
using System.Collections;

public class WeaponReferenceController : MonoBehaviour {
    
    public CameraController cameraController;
    public PlayerController playerController;
    public WeaponController weaponController;
    Vector3 weapPosFP;
    Vector3 weapPosTP;

	// Use this for initialization
	void Start () {
        weapPosTP = Vector3.Scale(transform.localPosition - playerController.transform.localPosition,new Vector3(0,1,0));
        weapPosFP = transform.localPosition - playerController.transform.localPosition;

	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (cameraController.controlMode == 0)
        {
            transform.rotation = cameraController.camRot;
            transform.localPosition = weapPosFP + playerController.transform.localPosition;

        }
        else if (cameraController.controlMode == 1 && playerController.moving == false && weaponController.shooting == false)
        {
            transform.localRotation = Quaternion.Euler(Vector3.Scale(transform.localRotation.eulerAngles, new Vector3(0,1,0)) + Vector3.Scale(cameraController.camRot.eulerAngles, new Vector3(1, 0, 1)));
            transform.localPosition = weapPosTP + playerController.transform.localPosition;
        }
        else if (cameraController.controlMode == 1 && (playerController.moving == true || weaponController.shooting == true))
        {
            transform.localRotation = Quaternion.Euler(playerController.transform.localRotation.eulerAngles + Vector3.Scale(cameraController.camRot.eulerAngles,new Vector3(1,0,1)));
            transform.localPosition = weapPosTP + playerController.transform.localPosition;
            weaponController.shooting = false;
        }

	}
}
