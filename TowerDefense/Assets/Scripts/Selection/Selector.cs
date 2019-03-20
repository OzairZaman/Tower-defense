using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public GameObject[] towers;
    public GameObject[] holograms;

    private int currentTower = 0;
    private int currentHolo = 0;

    private Vector3 placablePoint;

    private void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(placablePoint, .5f);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit))
        {
            //try
            Placable p = hit.collider.GetComponent<Placable>();
            if (p)
            {
                placablePoint = p.transform.position;
            }
        }
	}

    public void SelectTower(int _tower)
    {
        //is _tower with in range of array
        if (_tower >= 0 && _tower < towers.Length)
        {
            // set currenttower to tower
            currentTower = _tower;
        }

    }
}
