using UnityEngine;
using System.Collections;

public class Bullet2Controller : MonoBehaviour {

    // Variables
    Vector3 dir;
    Vector3 prevPos;
    float speed = 40;
    public GameObject particles;
    float destroySpeed = 20;
    float destroyTorque = 500;

	// Use this for initialization
	void Start () {
        prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit hit;

        // Check wheter bullet hit something.
        if (Physics.Raycast(prevPos, dir, out hit, Vector3.Distance(prevPos, transform.position)))
        {
            // If so check whether hit object has a rigidbody.
            if (hit.collider.GetComponent<Rigidbody>() != null)
            {
                // If so give the rigidbody a velocity in the same direction as the bullet and give it a rotational speed due to torques.
                Rigidbody rigid = hit.collider.GetComponent<Rigidbody>();

                rigid.maxAngularVelocity = Mathf.Infinity;
                rigid.velocity = dir * destroySpeed + rigid.velocity;
                rigid.AddTorque((Vector3.Cross(transform.position - hit.transform.position,Vector3.Normalize(dir))) * destroyTorque, ForceMode.Impulse);

            }
            // Create a particle system and destory the bullet.
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        prevPos = transform.position;

        // When bullet is out of sight remove it.
        if (Vector3.Distance(transform.position, transform.parent.FindChild("PlayerObjects").position) > 200f) Destroy(this.gameObject);
	}

    void FixedUpdate()
    {
        // Move in desired direction.
        transform.position = dir * speed * Time.fixedDeltaTime + transform.position;
    }

    // Method to set the direction of the bullet.
    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }

}
