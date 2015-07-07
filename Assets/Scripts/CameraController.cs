﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Public variables.
    public GameObject player;
    public float rotateSpeedFP;
    public float rotateSpeedTP;

    // Private variables.
    int controlMode;
    Vector3 rotation;
    public Quaternion camRot = Quaternion.identity;
    Vector3 camMov;
    RaycastHit hit;
    float camDis = 6;
    float camOffset = 1.5f;
    float distanceOffset;
    float scrollSpeed = 4;
    float maxCamHeight = 10;
    float minCamHeight = 0;
    float x;
    float y;
    public Vector3 FPCameraPos;
    LayerMask mask = ~(1 << 8);

	// Method starts when object is generated.
	void Start () {
        // Setting to first person at the start of the game.
        controlMode = 0;
        // Initializing camMov.
        camRot = transform.rotation;
        // Initializing FPCameraPos
        FPCameraPos = transform.localPosition;
	}
	
    // Repeating method which is called continuously
	void FixedUpdate () {


        // Switching between first and third person mode
        if (Input.GetKeyDown(KeyCode.V) && controlMode == 0) controlMode = 1;
        else if (Input.GetKeyDown(KeyCode.V) && controlMode == 1)
        {
            controlMode = 0;
        }
	}

    // Repeating method which is called after physics calculation
    void LateUpdate()
    {
        // Use first or third person mode.
        if (controlMode == 0) FirstPerson();
        else if (controlMode == 1) ThirdPerson();

        if (controlMode == 1) transform.position = camMov;
        if (controlMode == 0)
        {
            this.transform.localPosition = FPCameraPos;
        }
        // Updating camera rotation
        transform.rotation = camRot;
    }

    // Control method for first person camera.
    void FirstPerson()
    {
        // Getting the mouse input and camera angle
        float RotationVertical = rotateSpeedFP * Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime;
        float RotationHorizontal = rotateSpeedFP * Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime;

        // summing the rotation with the new rotation ordered by the mouse changes and transform the camera angles with this
        rotation = rotation + new Vector3(-RotationHorizontal, RotationVertical, 0f)*Time.deltaTime;

        // limiting the movement of the camera
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

        camRot = Quaternion.Euler(rotation);
    }

    // Control method for third person camera.
    void ThirdPerson()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            // change camera height according to input from mouse scrollwheel and setting limits
            camDis = Mathf.Clamp(camDis - Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed, minCamHeight, maxCamHeight);
        }

        // determining input from mouse axis
        float mousex = Input.GetAxisRaw("Mouse X") * rotateSpeedTP * Time.fixedDeltaTime;
        float mousey = Input.GetAxisRaw("Mouse Y") * rotateSpeedTP * Time.fixedDeltaTime;

        // updating x and y with mouse movement. Limiting the y movement.
        x = x + mousex;
        y = Mathf.Clamp(y - mousey, -90, 90);

        // calculating the orbit around the player and setting rotation of camera
        camRot = Quaternion.Euler(y, x, 0f);

        // calculating position of camera and setting position of camera
        Vector3 probPosition = camRot * new Vector3(0f, 0f, -camDis) + player.transform.position;
        // Checking for collision from camera with objects with method
        CamCollide(probPosition);

        // calculating position of camera and setting position of camera
        Vector3 position = camRot * new Vector3(0f, 0f, -camDis + distanceOffset) + player.transform.position;

        camMov = position + new Vector3(0f, camOffset, 0f);
    }

    // method for determining if the camera got through an object
    private void CamCollide(Vector3 Position)
    {
        // create a vector from the player to the camera
        Vector3 relativePos = Position - (player.transform.position);

        // casting a ray from player to camera and checking if it hit something
        if (Physics.Raycast(player.transform.position + new Vector3(0f, camOffset, 0f), relativePos, out hit, camDis + 0.5f, mask))
        {
            // setting an offset of the camera so it doesnt go through a wall
            distanceOffset = camDis - hit.distance + 0.8f;
            distanceOffset = Mathf.Clamp(distanceOffset, 0f, camDis);
        }
        // else a normal distance offset
        else
        {
            distanceOffset = 0f;
        }

    }


}