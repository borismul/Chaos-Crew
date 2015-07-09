using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {

    float timeStart;
	// Use this for initialization
	void Start () 
    {
        timeStart = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float time = Time.realtimeSinceStartup - timeStart;
        if (time > 0.1f) Destroy(this.gameObject);
	}
}