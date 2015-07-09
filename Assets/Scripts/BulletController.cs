using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    Vector3 dir;
    Vector3 prevPos;
    float speed = 20;
    public GameObject particles;
    public GameObject player;
	// Use this for initialization
	void Start () {
        prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = dir * speed * Time.deltaTime + transform.position;
        RaycastHit hit;

        if (Physics.Raycast(prevPos, dir, out hit, Vector3.Distance(prevPos, transform.position)))
        {
            if (hit.collider.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rigid = hit.collider.GetComponent<Rigidbody>();
                rigid.velocity = dir * speed + rigid.velocity;
            }
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        prevPos = transform.position;

        if (Vector3.Distance(transform.position, transform.parent.FindChild("PlayerObjects").position) > 200f) Destroy(this.gameObject);
	}

    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }

}
