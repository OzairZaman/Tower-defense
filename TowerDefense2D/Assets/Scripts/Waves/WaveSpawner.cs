using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {


    //our spawn state enum
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    
    
    //we are serializing this class so unity can use public arrays of this in the UI
    [System.Serializable]
    public class Wave
    {

        public string name; //name of the wave
        public Transform enemy; //reference to the prefab that will spawn
        public int count; // number of enemies to spawn
        public float rate;

    }


    //variables
    public Wave[] waves;
    private int nextWave = 0;
    public SpawnState spawnState = SpawnState.COUNTING; //default our enum to COUNTING
    public float timeBetweenWaves = 5f;
    public float countdown;
    //this is the 1 second time delay for checking if any all enemy dead after a wave
    private float searchEnemyCountdown = 1f;

    private void Start()
    {
        countdown = timeBetweenWaves;
    }

    private void Update()
    {

        //WAITING is set after all wave has spawned during SPAWNING
        //
        //
        if (spawnState == SpawnState.WAITING)
        {
            // check if enemies are still alive
            if (!EnemyIsAlive())
            {
                
                Debug.Log("stage is clear of enemies - Call LetsDoItAgain()");
                LetsDoItAgain();
                
            }
            else
            {
                return;
            }

        }


        if (countdown < 0)
        {
            if (spawnState != SpawnState.SPAWNING)
            {
                //start spawning waves
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            //countdown is not zero so lets count it down!
            countdown -= Time.deltaTime;
        }
    }

    //Note: Systems.Collections + IEnumerator are needed to use time wait methods
    //instead of void this method returns IEnumerator
    //takes a Wave as an argument
    IEnumerator SpawnWave(Wave _wave)
    {

        Debug.Log("Spawning wave :" + _wave.name + "with " + _wave.count + "enemies");
        spawnState = SpawnState.SPAWNING;
        
        for (int i = 0; i < _wave.count; i++)
        {
            // call SpawnEnemy
            //enemy in the Wave class is the reference to the prefeab of a particular enemy type.
            SpawnEnemy(_wave.enemy);

            //now we want to wait before we iterate again in this for loop
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        spawnState = SpawnState.WAITING;

        yield break; //end method and satisfy return condition of method
    }

    void SpawnEnemy(Transform _enemy)
    {
        //spawn the enemy
        Debug.Log("Spawn Enemy: " + _enemy.name);
        int ran = Random.Range(0, 4);
        Vector3 pos = new Vector3(9, 2 * ran - 3, -1);
        Instantiate(_enemy, pos, Quaternion.identity);
    }

    bool EnemyIsAlive()
    {
        //count down the search enemy timer
        searchEnemyCountdown -= Time.deltaTime;

        if (searchEnemyCountdown < 0)
        {
            searchEnemyCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {

                return false;
            }
        }

        

        return true;
    }


    //this is called to go to next wave 
    void LetsDoItAgain()
    {
        //begin a new wave
        //do all the stuff needed to set up a new wave


        //let SpawnState to counting
        spawnState = SpawnState.COUNTING;
        //reset countdown
        countdown = timeBetweenWaves;
       //if last wave  then reset nextWave to 0 (or end game)
            //nextWave + 1
        
        if (nextWave+1 > waves.Length-1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES ARE DESTROYED!!");
        }
        else
        {
            //increment nextWave
            nextWave++;
        }

    }

}
