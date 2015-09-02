using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncPlayerTransform : NetworkBehaviour {

    [SyncVar]
    Vector3 syncPos;

    [SyncVar]
    Quaternion syncPlayerRot;

    [SyncVar]
    Quaternion syncCameraRot;
    
    public Transform myTransform;
    public Transform cameraTransform;
    float lerpRate = 15;
	
	void FixedUpdate () {

        TransmitPosition();
        LerpPosition();
	}

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.fixedDeltaTime * lerpRate);
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncPlayerRot, Time.fixedDeltaTime * lerpRate);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, syncCameraRot, Time.fixedDeltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 Pos, Quaternion playerRotation, Quaternion cameraRotation)
    {
        syncPos = Pos;
        syncPlayerRot = playerRotation;
        syncCameraRot = cameraRotation;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
            CmdProvidePositionToServer(myTransform.position, myTransform.rotation, cameraTransform.rotation);
    }
}
