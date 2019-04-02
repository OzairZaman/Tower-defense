using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTower : MonoBehaviour {

    public int selected;
    public GameObject[] towers;
    public float[] prices;
    public GameObject Path;
    private Money moneyScript;
    
    // Use this for initialization
	void Start () {
        moneyScript = GameObject.Find("WaveManager").GetComponent<Money>();
	}
	
	// Update is called once per frame
	void Update () {
        //create out camera ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            //check if what we hit has a tag named Path
            //Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Path")
            {
                Path = hit.transform.gameObject; 
            }
            else
            {
                Path = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && Path != null)
        {
            //get access to the TileTaken script for the Path hit by ray cast
            TileTaken tileTakenScript = Path.GetComponent<TileTaken>();
            if (!tileTakenScript.isTaken)
            {
                //do costing / money adjustments
                //get a position based on the Path we hit
                Vector3 pos = new Vector3(Path.transform.position.x, Path.transform.position.y, -1f);
                tileTakenScript.Tower = (GameObject)Instantiate(towers[selected], pos, Quaternion.identity);
                tileTakenScript.isTaken = true;
            }
        }
	}
}
