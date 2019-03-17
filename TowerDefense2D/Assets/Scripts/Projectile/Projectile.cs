using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float movementSpeed;
    public float Damage;
    Vector3 spawnPos;
    GameObject explosionEffect;
    
    // Use this for initialization
	void Start () {
        //setting it to its own position(why)
        spawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //move the projectile to the right at speed
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        //check if distance traveled is greater than 20
        if (Vector3.Distance(transform.position, spawnPos) > 15)
        {
            //destroy self
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log(other.tag);
            other.GetComponent<Health>().health -= Damage;
            Destroy(gameObject);
        }
    }
}
