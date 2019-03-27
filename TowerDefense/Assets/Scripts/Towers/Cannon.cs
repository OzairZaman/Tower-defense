using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cannon : Tower {

    public Transform orb; // Reference to orb for rotation
    public float lineDelay = .2f; // How long the line appears before disapearing
    public float rotationSpeed = 5f;
    public LineRenderer line; // Reference to line renderer

    void Reset()
    {
        // Gets a reference to line renderer
        line = GetComponent<LineRenderer>();
    }

    // Shows the line renderer for a split second (delay)
    IEnumerator ShowLine(float delay)
    {
        line.enabled = true;
        yield return new WaitForSeconds(delay);
        line.enabled = false;
    }

    // Rotates Orb to Enemy
    public override void Aim(Enemy e)
    {
        // Get orb to look at enemy
        orb.LookAt(e.transform);
        //Vector3 direction = e.transform.position - orb.position;
        //orb.rotation = Quaternion.Lerp(orb.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        
        // Create line from orb to enemy
        line.SetPosition(0, orb.position);
        line.SetPosition(1, e.transform.position);
    }

    // Deals damage to enemy and shows line
    public override void Attack(Enemy e)
    {
        // Show the line
        StartCoroutine(ShowLine(lineDelay));
        // Deal damage
        e.TakeDamage(damage);
    }
}
