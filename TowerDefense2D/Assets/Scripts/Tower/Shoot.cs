using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float cooldownAmount;
    private float cdCounter;
    public GameObject projectile;
    
    
    // Use this for initialization
	void Start () {
        cdCounter = cooldownAmount;
    }
	
	// Update is called once per frame
	void Update () {
        if (cdCounter > 0)
        {
            //cdCounter is not zero yet sooooo keep cooling down!!
            cdCounter -= Time.deltaTime;
        }
        else
        {
            //cdCounter is zero so lets shooot!!
            
            //create a projectile object (AKA shoooosssst)
            // BUT !!
            // Only do this if there is an enemy detected (lets Ray cast )
            RaycastHit hit; // to capture what the fuck we hit!
            //Debug.Log(Physics.Raycast(transform.position, Vector3.right, out hit, 15));
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 15))
            {
                //Debug.Log(hit.transform.tag);

                if (hit.transform.tag == "Enemy")
                {
                    //reset cdCounter to our cd value
                    cdCounter = cooldownAmount;
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
                
            }
            
        }
	}
}
