using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public CameraController cameraController;
    float speed = 300;
    float jumpSpeed = 10f;
    bool jumping;
    float creativeUpDownSpeed = 250;
    float velY;
    int spacePresses;
    Vector3 prevPos;
    Vector3 prevMove;
    int chunkWidth;
    int chunkHeight;

	// Use this for initialization
	void Start () {
 
	}

    void Update()
    {

        Jump();

    }
	// Method that is called continuously
	void FixedUpdate () {
        rigidbody.velocity = Movement();
        transform.rotation = Rotation();
	}
    // Checking whether player touches the ground
    bool IsGrounded()
    {
        // cast 5 rays around the player
        RaycastHit hit;
        bool cond1 = Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f);
        bool cond2 = Physics.Raycast(transform.position + new Vector3(0.4f, 0f, 0f), Vector3.down, out hit, 1.51f);
        bool cond3 = Physics.Raycast(transform.position + new Vector3(-0.4f, 0f, 0f), Vector3.down, out hit, 1.51f);
        bool cond4 = Physics.Raycast(transform.position + new Vector3(0, 0f, 0.4f), Vector3.down, out hit, 1.51f);
        bool cond5 = Physics.Raycast(transform.position + new Vector3(0, 0f, -0.4f), Vector3.down, out hit, 1.51f);

        // see whether one of them hits the ground and if player is not jumping
        if ((cond1 || cond2 || cond3 || cond4 || cond5) && rigidbody.velocity == Vector3.zero)
        {
            //transform.position = transform.position + new Vector3(0, 1.5f - (transform.position.y - hit.point.y), 0);
        }

        return cond1 || cond2 || cond3 || cond4 || cond5;
    }

    Vector3 Movement()
    {

        if (!IsGrounded() || jumping)
        {
            velY += Physics.gravity.y * Time.fixedDeltaTime;
        }
        else if (!jumping && prevMove.y < 0)
        {
            velY = 0;
        }

        Vector3 movement = new Vector3(0f, velY, 0f);

        float InHorz = Input.GetAxisRaw("Horizontal") * speed;
        float InVert = Input.GetAxisRaw("Vertical") * speed;

        movement = (transform.forward * InVert + transform.right * InHorz) * Time.fixedDeltaTime + movement;

        //if (IsGrounded() && movement == Vector3.zero)
        //{
        //    transform.position = prevPos + prevMove * Time.fixedDeltaTime;
        //}

        prevPos = transform.position;
        prevMove = movement;

        return movement;

    }

    Quaternion Rotation()
    {

        Vector3 camEulerRotation = cameraController.transform.rotation.eulerAngles;
        Vector3 playerEulerRotation = new Vector3(0f, camEulerRotation.y, 0f);

        return Quaternion.Euler(playerEulerRotation);
    }

    void Jump()
    {
        
        jumping = false;
        if (Input.GetKey(KeyCode.Space) && IsGrounded() && rigidbody.velocity.y <= 0)
        {
            velY = jumpSpeed;
            jumping = true;
        }

    }
}
