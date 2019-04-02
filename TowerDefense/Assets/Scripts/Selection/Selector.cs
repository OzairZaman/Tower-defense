using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    public Transform towerParent; // cosmetics only
    
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


    /// <summary>
    /// Disables the GameObjects of all referenced holograms
    /// </summary>
    void DisableAllHolograms()
    {
        // Loop through all Holograms
        foreach (var holo in holograms)
        {
            // Disable hologram
            holo.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        DisableAllHolograms();
	}
	
	// Update is called once per frame
	void Update () {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit) == true)
        {
            //we know -
            // it hit something
            // what ever it hit, that info is stored in 'hit' variable
            //(lowkey making an assumtion - what is that? we hit a tile)
            // tile has our Placable
            // lets try to get that
            Placable p = hit.collider.GetComponent<Placable>();

            if (p != null && p.isAvailable == true) //if we were able to actually get the Placable component
            {
                //>>Hover Mechanic<<
                // Get hologram of current tower
                GameObject holgram = holograms[currentHolo];
                // Activate hologram
                holgram.SetActive(true);
                // Position hologram to tile
                holgram.transform.position = p.GetPivotPoint();

                //>>Placement Mechanic<<
                // If left mouse is down
                if (Input.GetMouseButtonDown(0))
                {
                    //      Get the current tower prefab
                    GameObject tower = towers[currentTower];
                    //      Spawn a new tower
                    GameObject clone = Instantiate(tower, towerParent);
                    //      Position new tower to tile
                    clone.transform.position = p.GetPivotPoint();

                    //clone.po
                    //      Tile is no longer placeable
                    p.isAvailable = false;
                }

            }
            
        }
        else
        {
            DisableAllHolograms();
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
