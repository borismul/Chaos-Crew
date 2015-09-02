using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {

    // Public variables.
    public GameObject player;
    public GameObject[] playerObjects;
    public GameObject thisCamera;

    // Private variables.
    public int controlMode;
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
    Vector3 FPCameraPos;
    LayerMask mask = ~(1 << 8);
    float rotateSpeedFP = 75;
    float rotateSpeedTP = 75;

	// Method starts when object is generated.
	void Start () {

        if (!isLocalPlayer)
        {
            SetVisible();
            return;
        }

        thisCamera.SetActive(true);

        Cursor.visible = false;
        // Setting to first person at the start of the game.
        controlMode = 0;
        // Initializing camMov.
        camRot = thisCamera.transform.rotation;
        // Initializing FPCameraPos
        FPCameraPos = thisCamera.transform.localPosition;
	}
	
    // Repeating method which is called continuously
	void Update () {

        if (!isLocalPlayer) return;
        // Switching between first and third person mode
        if (Input.GetKeyDown(KeyCode.V) && controlMode == 0)
        {
            SetVisible();
            controlMode = 1;
        }
        else if (Input.GetKeyDown(KeyCode.V) && controlMode == 1)
        {
            foreach (GameObject playerPart in playerObjects)
            {
                playerPart.SetActive(false);
            }
            controlMode = 0;
            // Set camera position to right spot.
        }

	}

    // Repeating method which is called after physics calculation
    void LateUpdate()
    {
        // If in first person mode
        if (controlMode == 0)
        {
            // Determine the camera rotation
            FirstPerson();

            thisCamera.transform.localPosition = FPCameraPos;


        }
        // If in third person mode
        if (controlMode == 1)
        {
            // Calculate position and rotation of camera
            ThirdPerson();

            // Set camera position
            thisCamera.transform.position = camMov;
        }


        // Set camera rotation
        thisCamera.transform.rotation = camRot;
    }

    // Control method for first person camera.
    public void FirstPerson()
    {
        // determining input from mouse axis
        float mousex = Input.GetAxisRaw("Mouse X") * rotateSpeedFP * Time.fixedDeltaTime;
        float mousey = Input.GetAxisRaw("Mouse Y") * rotateSpeedFP * Time.fixedDeltaTime;

        // updating x and y with mouse movement. Limiting the y movement.
        x = x + mousex;
        y = Mathf.Clamp(y - mousey, -90, 90);

        // calculating rotation of camera
        camRot = Quaternion.Euler(y, x, 0f);

    }

    // Control method for third person camera.
    public void ThirdPerson()
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
        Vector3 probPosition = camRot * new Vector3(1f, 1f, -camDis) + player.transform.position;

        // Checking for collision from camera with objects with method
        CamCollide(probPosition);

        // calculating position of camera and setting position of camera
        Vector3 position = camRot * new Vector3(1f, 1f, -camDis + distanceOffset) + player.transform.position;

        camMov = position + new Vector3(0f, camOffset, 0f);
    }

    // Method for determining if there is an object between the player and the camera
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

    void SetVisible()
    {
        foreach (GameObject playerPart in playerObjects)
        {
            playerPart.SetActive(true);
        }
    }

}
