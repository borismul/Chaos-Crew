using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Variables.
    public CameraController cameraController;
    public GameObject mainCamera;
    public WeaponController weaponController;
    public bool moving;
    float speed = 500;
    float jumpSpeed = 10f;
    bool jumping;
    float velY;
    int spacePresses;
    Vector3 prevPos;
    Vector3 prevMove;
    int chunkWidth;
    int chunkHeight;
    float InHorz;
    float InVert;

	// Use this for initialization.
	void Start () {
        // Initiating some variables.
        prevPos = transform.position;
        prevMove = Vector3.zero;
        InHorz = 0;
        InVert = 0;
	}

    void Update()
    {
        // Checking whether the player wants to jump.
        Jump();

        // if fp mode determine the orientation of the player.
        if (cameraController.controlMode == 0)
        {
            transform.localRotation = Rotation();

        }
        // else if tp mode determine orientation of player when it is moving.
        if (cameraController.controlMode == 1 && (InHorz != 0 || InVert != 0) || weaponController.shooting == true)
        {
            Vector3 movement = Vector3.Normalize(Vector3.Scale(cameraController.transform.forward, new Vector3(1, 0, 1)));
            transform.rotation = Quaternion.LookRotation(movement);
            
        }
    
        
        
    }
	// Method that is called continuously.
	void FixedUpdate () {

        // Setting the velocity of the rigidbody.
        GetComponent<Rigidbody>().velocity = Movement();


	}
    // Checking whether player touches the ground.
    bool IsGrounded()
    {
        // cast 5 rays around the player.
        RaycastHit hit;
        bool cond1 = Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f);
        bool cond2 = Physics.Raycast(transform.position + new Vector3(0.4f, 0f, 0f), Vector3.down, out hit, 1.51f);
        bool cond3 = Physics.Raycast(transform.position + new Vector3(-0.4f, 0f, 0f), Vector3.down, out hit, 1.51f);
        bool cond4 = Physics.Raycast(transform.position + new Vector3(0, 0f, 0.4f), Vector3.down, out hit, 1.51f);
        bool cond5 = Physics.Raycast(transform.position + new Vector3(0, 0f, -0.4f), Vector3.down, out hit, 1.51f);

        // see whether one of them hits the ground and if player is not jumping.
        if ((cond1 || cond2 || cond3 || cond4 || cond5) && GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            transform.position = transform.position + new Vector3(0, 1.5f - (transform.position.y - hit.point.y), 0);
        }

        return cond1 || cond2 || cond3 || cond4 || cond5;
    }

    // Method for determining the players movements.
    Vector3 Movement()
    {
        // If player is not on the ground or the player is jumping.
        if (!IsGrounded() || jumping)
        {
            // determine its y velocity
            velY += Physics.gravity.y * Time.fixedDeltaTime;
        }
        // If it is on the ground, is not jumping and its previous velocity was lower than 0, set y velocity to 0.
        else if (!jumping && prevMove.y < 0)
        {
            velY = 0;
        }

        // Put this y velocity in a vector3.
        Vector3 movement = new Vector3(0f, velY, 0f);
        
        // Determine the inputs from the player.
        InHorz = Input.GetAxisRaw("Horizontal");
        InVert = Input.GetAxisRaw("Vertical");

        if (InHorz != 0 || InVert != 0) moving = true;
        else moving = false;

        // Put these inputs in the right direction so the player moves in the desired direction.
        movement = Vector3.Normalize(Vector3.Scale(cameraController.transform.forward * InVert + cameraController.transform.right * InHorz, new Vector3(1, 0, 1))) *Time.fixedDeltaTime * speed + movement;

        // If the player is grounded and its movement is 0
        if (IsGrounded() && movement == Vector3.zero)
        {
            // Set player back to the position it was, so it doesnt move at all.
            transform.position = prevPos + prevMove * Time.fixedDeltaTime;
        }

        // save the the movement and position of this iteration.
        prevPos = transform.position;
        prevMove = movement;

        // return the movement.
        return movement;

    }

    // Method for determining the rotation of the player.
    Quaternion Rotation()
    {

        // Rotate the player in the same direction as the camera along the y axis.
        Vector3 camEulerRotation = cameraController.transform.rotation.eulerAngles;
        Vector3 playerEulerRotation = new Vector3(0f, camEulerRotation.y, 0f);

        // Return this rotation.
        return Quaternion.Euler(playerEulerRotation);
    }

    void Jump()
    {
        // set jumping false every iteriation.
        jumping = false;

        // determine whether the player pressed space and is on the ground and has no y velocity yet.
        if (Input.GetKey(KeyCode.Space) && IsGrounded() && GetComponent<Rigidbody>().velocity.y <= 0)
        {
            // if so set the y velocity to the jumpspeed and set jumping to true.
            velY = jumpSpeed;
            jumping = true;
        }

    }
}
