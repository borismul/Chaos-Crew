using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    int weapon;
    public GameObject[] weaponGameObjects;
    public float hitSpeed;
    LayerMask groundLayer = ~(1 << 9) & ~(1 <<8);

	// Start runs upon creation of player
	void Start () {
        // start with weapon 1
        weapon = 1;
	}
	
	// Update is called once per frame
	void Update () {
        CheckWeapon();

        if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();
	}

    // Method checks weather player wants to switch weapons.
    void CheckWeapon(){
        // Check whether player want to change weapons
        if (Input.GetKeyDown(KeyCode.Alpha0)) weapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) weapon = 1;

        // Set weapon that is desired active and deactivate all others.
        for (int i = 0; i < weaponGameObjects.Length; i++)
        {
            if (i == weapon) weaponGameObjects[i].SetActive(true);
            else weaponGameObjects[i].SetActive(false);

        }
    }

    void Shoot()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 25f, groundLayer);
        foreach (Collider hit in hits)
        {
            float distance = Vector3.Distance(hit.transform.position, transform.position);
            Vector3 direction = Vector3.Normalize(hit.transform.position-transform.position);
            hit.GetComponent<Rigidbody>().velocity = direction * hitSpeed / (distance);
        }
    }
}
