using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    int weapon;
    public GameObject[] weaponGameObjects;
    public float hitSpeed;
    public GameObject bullet1;
    public GameObject instantiateBullet1;
    public GameObject bullet2;
    public GameObject instantiateBullet2;
    public GameObject mainCamera;
    public CameraController cameraController;
    public bool shooting = false;
    bool checkShooting = false;
    bool shootNow = false;

	// Start runs upon creation of player
	void Start () {
        // start with weapon 1
        weapon = 1;
	}
	
	// Update is called once per frame
	void Update () {
        CheckWeapon();

        if (cameraController.controlMode == 0) if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

        if (cameraController.controlMode == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) shooting = true;

            if (shooting != checkShooting && shooting == false) shootNow = true;

            if (shootNow)
            {
                Shoot();
                shootNow = false;
            }
            checkShooting = shooting;
        }
	}

    // Method checks weather player wants to switch weapons.
    void CheckWeapon(){
        // Check whether player want to change weapons
        if (Input.GetKeyDown(KeyCode.Alpha0)) weapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) weapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) weapon = 2;


        // Set weapon that is desired active and deactivate all others.
        for (int i = 0; i < weaponGameObjects.Length; i++)
        {
            if (i == weapon) weaponGameObjects[i].SetActive(true);
            else weaponGameObjects[i].SetActive(false);

        }
    }

    // Method that does something when player shoots
    void Shoot()
    {
        if (weapon == 1)
        {
            GameObject bullet = (GameObject)Instantiate(bullet1, instantiateBullet1.transform.position, mainCamera.transform.rotation);
            bullet.transform.parent = this.transform.parent;
            bullet.GetComponent<Bullet1Controller>().SetDirection(Vector3.Normalize(mainCamera.transform.forward));


        }

        // If weapon 2 create a bullet and give it its velocity.
        if (weapon == 2)
        {
            GameObject bullet = (GameObject)Instantiate(bullet2, instantiateBullet2.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform.parent;
            bullet.GetComponent<Bullet2Controller>().SetDirection(Vector3.Normalize(mainCamera.transform.forward));
        }

        
    }
}
