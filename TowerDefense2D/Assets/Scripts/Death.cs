using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    //this Death script can be on Tower and Enemy.
    
    //so we need to check if it is on tower or Enemy.

    public bool isTower;
    private Health healthScript;
    private Money moneyScript;
    private EnemyStats enemyStatsScript;

	
    // Use this for initialization
	void Start () {
        healthScript = gameObject.GetComponent<Health>();
        if (!isTower)
        {
            enemyStatsScript = gameObject.GetComponent<EnemyStats>();
        }
        

        //the money script is on GameManager object, acts as game stats.
        //we could also use namespaces
        moneyScript = GameObject.Find("WaveManager").GetComponent<Money>();

	}
	
	// Update is called once per frame
	void Update () {
        if (healthScript.health < 0)
        {
            if (isTower)
            {
                //do tower death stuff
                Destroy(gameObject);
            }
            else
            {
                //give enemy resources
                moneyScript.money += enemyStatsScript.value;
                //do enemy death stuffs
                Destroy(gameObject);
            }
        }
	}
}
