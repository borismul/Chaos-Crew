using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    int weapon;
    public GameObject[] weaponGameObjects;
    public float hitSpeed;
    public GameObject bullet1;
    public GameObject instantiateBullet1;
    public GameObject mainCamera;

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
        if (weapon == 1)
        {
            GameObject bullet = (GameObject)Instantiate(bullet1, instantiateBullet1.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform.parent;
            bullet.GetComponent<BulletController>().SetDirection(Vector3.Normalize(mainCamera.transform.forward));

        }
    }
}
