using UnityEngine;
using System.Collections;

public class Bullet1Controller : MonoBehaviour {
    
    Vector3 dir;
    float destroySpeed = 20;
    float lifeTime;

	// Use this for initialization
	void Start () {
	    RaycastHit[] collides = Physics.SphereCastAll(new Ray(transform.position,dir),5,20);

        foreach (RaycastHit collide in collides)
        {
            Rigidbody rigid = collide.transform.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                rigid.velocity = dir * destroySpeed + rigid.velocity;
            }
        }
        lifeTime = Time.realtimeSinceStartup;
	}

    // Method to set the direction of the bullet.
    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }

    void Update()
    {

        if (Time.realtimeSinceStartup - lifeTime > 3f) Destroy(this.gameObject);
    }
}
