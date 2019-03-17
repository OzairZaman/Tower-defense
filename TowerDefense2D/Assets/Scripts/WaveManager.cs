using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {


    public int numberOut;
    public GameObject[] Enemies;
    public float cooldownAmount;
    public float cd;

    
    // Use this for initialization
	void Start () {
        cd = cooldownAmount * 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (cd > 0)
        {
            cd -= Time.deltaTime;

        }
        else
        {
            cd = cooldownAmount;
            int ran = Random.Range(0, 4);
            //Note: 2*x - 3 for (x:0, 1, 2, 3) gives -3, -1, 1, 3 for the 4 rows Y value.
            Vector3 pos = new Vector3(9, 2*ran-3, -1);
            int enemyIndex = Random.Range(0, Enemies.Length);
            Instantiate(Enemies[enemyIndex], pos, Quaternion.identity);
        }
	}
}
